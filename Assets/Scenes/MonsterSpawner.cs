using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monster;

    void Start()
    {
        // TODO: make this repeat
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            var screenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Screen.width,
                    Screen.height,
                    Camera.main.transform.position.z
                )
            );

            var pointToSpawn = new Vector3(
                UnityEngine.Random.Range(
                    -screenBounds.x * 0.8f,
                    screenBounds.x * 0.8f
                ),
                screenBounds.y * 1.1f,
                0.0f
            );

            Instantiate(monster, pointToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
