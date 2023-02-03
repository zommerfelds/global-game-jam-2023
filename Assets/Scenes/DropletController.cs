using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckEnemy();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            transform.Translate(0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            transform.Translate(0.0f, -0.1f, 0f);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            transform.Translate(0.0f, 0.1f, 0f);
        }
    }

    void CheckEnemy()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Enemy")
        {
            Debug.Log("Touched enemy!");
        }
    }
}
