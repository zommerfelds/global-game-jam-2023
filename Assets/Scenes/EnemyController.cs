using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Mathf.Sin(Time.realtimeSinceStartup * 3.0f) * 0.03f, -0.05f, 0f);
    }
}
