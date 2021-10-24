using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField]
    floatScriptableObject health;


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().fillAmount = health.value / 100;
    }
}
