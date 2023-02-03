using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        transform.Translate(Mathf.Sin(Time.realtimeSinceStartup * 3.0f) * 0.03f, -0.05f, 0f);
    }
}
