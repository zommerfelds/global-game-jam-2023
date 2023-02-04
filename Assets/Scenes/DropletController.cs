using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dropImage;

    private float velX = 0.0f;
    private static float VEL_X_DELTA = 0.02f;
    private static float VEL_X_MAX = 0.2f;

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
        // Move();
        // Animate();

        // Code for flappy bird style:
        Move2();
        Animate2();
    }

    void Move2()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            velX += VEL_X_DELTA;
        }
        else
        {
            velX -= VEL_X_DELTA;
        }
        velX = Mathf.Clamp(velX, -VEL_X_MAX, VEL_X_MAX);
        transform.Translate(velX, 0f, 0f);
    }

    void Animate2()
    {
        dropImage.transform.rotation = Quaternion.Euler(0, 0, 180
            - velX / VEL_X_MAX * 30 // face in the direction of speed
            + Mathf.Cos(Time.time * 30) * 5 // wiggle a bit
        );
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0.0f, -0.1f, 0f);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
