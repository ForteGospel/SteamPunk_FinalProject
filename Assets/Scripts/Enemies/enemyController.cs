using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class enemyController : MonoBehaviour 
{
    //Animator
    protected const string ANIM_MOVE = "speed";
    protected const string ANIM_ATTACK = "attack";
    protected const string ANIM_DIE = "die";
    protected Animator animator;

    [SerializeField]
    protected floatScriptableObject health;
    protected GameObject player;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float sightRange;
    [SerializeField]
    protected LayerMask whatIsPlayer;

    //Patrol
    protected NavMeshAgent agent;
    [SerializeField]
    protected Vector3 walkPoint;
    protected bool walkPointSet;
    protected float walkPointRange = 20f;
    [SerializeField]
    protected LayerMask whatIsGround;
    protected bool playerInSightRange, playerInAttackRange;

    //Attack
    [SerializeField]
    protected float timeBetweenAttacks;
    [SerializeField]
    protected bool alreadyAttacked;


    protected void Start()
    {
        player = GameObject.Find("PlayerArmature");
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    protected virtual void Patroling()
    {
        animator.SetFloat(ANIM_MOVE, agent.speed);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - new Vector3(walkPoint.x, walkPoint.y + agent.baseOffset, walkPoint.z);

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f || agent.pathStatus != NavMeshPathStatus.PathComplete)
            walkPointSet = false;
    }
    protected virtual void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y -agent.baseOffset, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    protected virtual void ChasePlayer()
    {
        animator.SetFloat(ANIM_MOVE, agent.speed);
        agent.SetDestination(player.transform.position);
    }

    protected virtual void AttackPlayer()
    {
        //Make sure enemy doesn't move
        walkPoint = transform.position;
        agent.SetDestination(transform.position);
        animator.SetFloat(ANIM_MOVE, 0f);
        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            animator.SetTrigger(ANIM_ATTACK);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    protected void TakeDamage(int damage)
    {
        health.value -= damage;

        if (health.value <= 0)
        {
            animator.SetTrigger(ANIM_DIE);
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }
    protected void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.DrawWireCube(walkPoint, Vector3.one * 3);
    }
}
