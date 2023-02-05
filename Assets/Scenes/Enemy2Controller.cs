using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
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
        var playerPos = GameObject.Find("Player").transform.position;
        var xTranslate = (transform.position.x > playerPos.x ? -1 : 1) * 0.05f;
        transform.Translate(xTranslate, -0.08f, 0f);
        if (transform.position.y < offscreenY)
        {
            Destroy(gameObject);
        }
    }
}
