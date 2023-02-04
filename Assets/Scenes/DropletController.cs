using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        Move();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched something: " + other.name);
        if (other.name.StartsWith("Enemy"))
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
