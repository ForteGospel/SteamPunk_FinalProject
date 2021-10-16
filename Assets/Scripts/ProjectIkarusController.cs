using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectIkarusController : MonoBehaviour
{
    [SerializeField]
    int[] secretCode;

    int[] codeEntered;

    int i = 0;

    [SerializeField]
    Color correctColor, errorColor;
    Color regularColor;

    // Start is called before the first frame update
    void Start()
    {
        codeEntered = new int[secretCode.Length];
        regularColor = GetComponentInChildren<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newNumberEntered(int number)
    {
        Debug.Log(number);
        codeEntered[i] = number;
        i++;
        if (i == secretCode.Length)
        {
            i = 0;

            if (isCodeCorrect())
            {
                feedBackCorrect();
            }
            else
            {
                feedBackError();
            }
        }
    }

    bool isCodeCorrect()
    {
        for (int j = 0; j < secretCode.Length; j++)
        {
            if (secretCode[j] != codeEntered[j])
                return false;
        }
        return true;
    }

    void feedBackError()
    {
        StartCoroutine(flashColors(errorColor));
    }

    void feedBackCorrect()
    {
        StartCoroutine(flashColors(correctColor));
    }

    IEnumerator flashColors(Color color)
    {
        float i = 0;
        while (i <= 2f)
        {
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material.color = color;
            yield return new WaitForSeconds(0.1f);
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material.color = regularColor;
            yield return new WaitForSeconds(0.1f);
            i += 0.2f;
        }
    }
}
