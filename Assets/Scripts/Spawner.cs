using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] 
    GameObject[] _prefab;

    public void Spawn()
    {
        Instantiate(_prefab[Random.Range(0, _prefab.Length)], transform.position, transform.rotation);
    }
}