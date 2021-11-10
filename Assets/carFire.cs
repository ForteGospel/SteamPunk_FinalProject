using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carFire : MonoBehaviour, IDamagable
{

    [SerializeField]
    GameObject[] gameObjects;

    int currObjectNum = 0;
    public void TakeDamage(Vector3 position)
    {
        enableNextEffect();
    }

    public void TakeRangeDamage()
    {
        enableNextEffect();
    }

    void enableNextEffect()
    {
        if (currObjectNum < gameObjects.Length)
        {
            gameObjects[currObjectNum].SetActive(true);
            currObjectNum++;
        }
    }
}
