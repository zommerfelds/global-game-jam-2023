using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
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
        transform.Translate(0.0f, -0.04f, 0f);
        var s = 3.0f + Mathf.Sin(Time.time * 1.0f + timeOffset) * 1.5f;
        transform.localScale = new Vector3(s, s, 1);
        if (transform.position.y < offscreenY * 2)
        {
            Destroy(gameObject);
        }
    }
}
