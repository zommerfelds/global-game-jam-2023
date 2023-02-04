using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dropImage;

    private float velX = 0.0f;
    private static float velXDelta = 0.001f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        // Code for normal arrow movement:
        Move();
        Animate();

        // Code for flappy bird style:
        // Move2();
    }

    void Move2()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            velX += velXDelta;
        }
        else
        {
            velX -= velXDelta;
        }
        transform.Translate(velX, 0f, 0f);
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

    void Animate()
    {
        dropImage.transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Cos(Time.time * 10) * 10);
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
