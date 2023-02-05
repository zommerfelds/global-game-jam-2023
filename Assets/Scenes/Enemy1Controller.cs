using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    private float timeOffset = 0.0f;
    private float offscreenY = 0.0f;

    void Start()
    {
        timeOffset = Random.Range(0.0f, Mathf.PI * 2);
        offscreenY = -Camera.main.ScreenToWorldPoint(
            new Vector3(
                Screen.width,
                Screen.height,
                Camera.main.transform.position.z
            )
        ).y * 1.1f;
    }

    void FixedUpdate()
    {
        transform.Translate(
            Mathf.Sin(Time.time * 3.0f + timeOffset) * 0.03f, -0.08f, 0f);
        if (transform.position.y < offscreenY * 2)
        {
            Destroy(gameObject);
        }
    }
}
