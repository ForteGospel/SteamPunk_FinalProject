using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossSpawner : MonoBehaviour
{
    [SerializeField]
    float spawnDelay = 2f;
    float timePassed = 0f;

    [SerializeField]
    Spawner[] spawnerObjects;

    private void Update()
    {
        if (timePassed >= spawnDelay)
        {
            timePassed = 0;
            foreach (Spawner sp in spawnerObjects)
                sp.Spawn();
        }

        timePassed += Time.deltaTime;
    }
}
