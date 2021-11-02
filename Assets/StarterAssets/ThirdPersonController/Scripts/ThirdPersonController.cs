using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class ThirdPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 5.335f;
		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[SerializeField]
		floatScriptableObject playerHealth, playerSteam;

		[SerializeField]
		float steamRechargeSpeed = 10f;

		[SerializeField]
		LayerMask whatIsInteractable;
		public float interactableSphereSize = 2f;
		

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.50f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Attack Variables")]
		public bool inAttack;
		public MeleeWeapon meleeWeapon;
		public GunWeapon gunWeapon;
		public Rig gunRig;

		private bool meeleeIsEnable = false;
		private bool rangeIsEnable = false;
		private bool wingsIsEnable = false;

		[Header("Damage Variables")]
		public float staggerTime = 1f;
		public float invinsibilityTime = 2f;
		public SkinnedMeshRenderer playerRenderer;
		public Color damageColor;
		private Color regularColor;
		private bool canGetHit = true;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;
		[Tooltip("For locking the camera position on all axis")]
		public bool LockCameraPosition = false;

		public GameObject wingsObject;
		public bool inSteam = false;

		// cinemachine
		private float _cinemachineTargetYaw;
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDJump;
		private int _animIDFreeFall;
		private int _animIDMotionSpeed;
		private int _animIDAttack;
		private int _animIDStateTime;
		private int _animIDHit;
		private int _animIDDeath;

		private Animator _animator;
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		private bool _hasAnimator;

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_hasAnimator = TryGetComponent(out _animator);
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();

			AssignAnimationIDs();

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			playerHealth.value = 100f;
			playerSteam.value = 50f;

			regularColor = playerRenderer.material.color;
		}

		private void Update()
		{
			_hasAnimator = TryGetComponent(out _animator);
			_animator.SetFloat(_animIDStateTime, Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));

			if (wingsObject.activeInHierarchy || _input.attack2)
            {
				if (playerSteam.value >= 0f)
					playerSteam.value -= steamRechargeSpeed * Time.deltaTime;
				else
					wingsObject.SetActive(false);

			}
			else
            {
				if (playerSteam.value <= 100f)
					playerSteam.value += steamRechargeSpeed * Time.deltaTime;
			}

			GroundedCheck();
			if(!PauseGameController.instance.isGamePaused)
            {
				AimAttack();
				ActivateObject();

				if (!inAttack)
				{
					RangeAttack();
					JumpAndGravity();
					Move();
				}
			}
		}



        private void LateUpdate()
		{
			CameraRotation();
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDStateTime = Animator.StringToHash("StateTime");
			_animIDAttack = Animator.StringToHash("MeleeAttack");
			_animIDHit = Animator.StringToHash("Hit");
			_animIDDeath = Animator.StringToHash("Death");
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDGrounded, Grounded);
			}
		}

		private void CameraRotation()
		{
			// if there is an input and camera position is not fixed
			if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
			{
				_cinemachineTargetYaw += _input.look.x * Time.deltaTime;
				_cinemachineTargetPitch += _input.look.y * Time.deltaTime;
			}

			// clamp our rotations so our values are limited 360 degrees
			_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Cinemachine will follow this target
			CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// move the player
			_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				wingsObject.SetActive(false);

				// update animator if using character
				if (_hasAnimator)
				{
					_animator.SetBool(_animIDJump, false);
					_animator.SetBool(_animIDFreeFall, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDJump, true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDFreeFall, true);
					}
					if (_input.jump && wingsIsEnable)
					{
						wingsObject.SetActive(!wingsObject.activeSelf);
					}
				}

				
				// if we are not grounded, do not jump
				_input.jump = false;


			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				if (wingsObject.activeInHierarchy)
                {
					if (inSteam)
						_verticalVelocity = 2f;
					else
						_verticalVelocity = -1f;
                }
				else
                {
					_verticalVelocity += Gravity * Time.deltaTime;
				}
			}
		}

		private void AimAttack()
        {
			if (_input.attack && playerSteam.value > 15f && Grounded && meeleeIsEnable)
            {
				_animator.SetTrigger(_animIDAttack);
				_input.attack = false;
				inAttack = true;
				playerSteam.value -= 15f;
			}
        }

		private void RangeAttack()
		{
			if (_input.attack2 && playerSteam.value > 15f && rangeIsEnable)
            {
				gunRig.weight += Time.deltaTime * 5f;
				if (gunRig.weight >= 1f)
                {
					gunWeapon.fireGun();
				}
					
            }
			else
            {
				gunRig.weight -= Time.deltaTime * 10f;
				gunWeapon.holdGun();
			}
		}

		public void enableMelee()
        {
			meeleeIsEnable = true;
			meleeWeapon.gameObject.SetActive(true);
        }

		public void enableRange()
		{
			rangeIsEnable = true;
			gunWeapon.gameObject.SetActive(true);
		}

		public void enableWings()
		{
			wingsIsEnable = true;
			wingsObject.gameObject.SetActive(true);
		}

		private void ActivateObject()
		{
			if (_input.activate)
            {
				_input.activate = false;
				Vector3 sphereCenter = new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z);
				Collider[] collider = Physics.OverlapSphere(sphereCenter, interactableSphereSize, whatIsInteractable);
				
				Debug.Log(collider.Length);
				if (collider.Length > 0)
					foreach (IInteractable interactable in collider[0].transform.GetComponents<IInteractable>())
						interactable.Interact();

			}
		}

		public void getHit(float damage)
        {
			if (canGetHit)
            {
				_animator.SetTrigger(_animIDHit);
				playerHealth.value -= damage;

				if (playerHealth.value < 0f)
				{
					//isdead
				}
				else
				{
					StartCoroutine("staggerPlayer");
				}
			}
        }

		private IEnumerator staggerPlayer()
        {
			canGetHit = false;
			float i = 0;
			while (i < invinsibilityTime)
            {
				playerRenderer.material.color = damageColor;
				yield return new WaitForSeconds(0.1f);
				playerRenderer.material.color = regularColor;
				yield return new WaitForSeconds(0.1f);
				i += 0.4f;
            }
			canGetHit = true;
        }

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}