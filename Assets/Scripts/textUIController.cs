using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class textUIController : MonoBehaviour
{
    [SerializeField]
    floatScriptableObject floatObject;

    [SerializeField]
    string text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = text + (int)Convert.ToSingle(floatObject.value);
    }
}
