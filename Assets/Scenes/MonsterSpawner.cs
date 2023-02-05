using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monster1;
    public GameObject monster2;

    public int currentLevel;

    void Start()
    {
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

            Debug.Log("New monster for level " + currentLevel);

            var monster = monster1;
            if (currentLevel == 2)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5)
                {
                    monster = monster2;
                }
            }
            Instantiate(monster, pointToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
