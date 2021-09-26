using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour {

    public Animator animator;
    private Transform toggleGroupTM;

	// Use this for initialization
	void Start () {
        toggleGroupTM = GameObject.Find("UI/ToggleGroup").transform;
	}

    public void ToggleChanged()
    {
        for (int i = 0; i < toggleGroupTM.childCount; i++)
        {
            if (toggleGroupTM.GetChild(i).GetComponent<Toggle>().isOn)
            {
                animator.SetInteger("AnimNum", i);
            }
        }
    }
}
