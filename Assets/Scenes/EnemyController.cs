using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float timeOffset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0.0f, Mathf.PI * 2);
    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        transform.Translate(Mathf.Sin(Time.time * 3.0f + timeOffset) * 0.03f, -0.05f, 0f);
    }
}
