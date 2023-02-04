using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dropImage;
    public GameObject narrationArea;
    public Text storyText;
    public bool gamePaused;

    public int score = 0;

    private float velX = 0.0f;
    private float velXDelta = 0.02f;

    private static float VEL_X_MAX = 0.2f;

    private void Update()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        }

        if (RestartGame())
        {
            Time.timeScale = 1;

            score = 0;
            gamePaused = false;
            narrationArea.SetActive(false);
            gameOverScreen.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (!gamePaused)
        {
            score += 1;
        }
    }

    private void FixedUpdate()
    {
        if (!gamePaused)
        {
            Move();
            Animate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched something: " + other.name);
        if (other.name.StartsWith("Enemy"))
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            gamePaused = true;
        }
    }

    private void Move()
    {
        velX = ResistFall() ? velX + velXDelta : velX - velXDelta;

        if (ShouldGoDown())
        {
            transform.Translate(0.0f, -0.1f, 0f);
        }
        else if (ShouldGoUp())
        {
            transform.Translate(0.0f, 0.1f, 0f);
        }
        else if (ShouldGoLeft())
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
        else if (ShouldGoRight())
        {
            transform.Translate(0.1f, 0f, 0f);
        }

        velX = Mathf.Clamp(velX, -VEL_X_MAX, VEL_X_MAX);
        transform.Translate(velX, 0f, 0f);
    }

    private void Animate()
    {
        dropImage.transform.rotation = Quaternion.Euler(0, 0, 0
            - velX / VEL_X_MAX * 30 // face in the direction of speed
            + Mathf.Cos(Time.time * 30) * 5 // wiggle a bit
        );
    }

    private bool RestartGame()
    {
        return gamePaused && Input.GetKey(KeyCode.Return);
    }

    private bool ResistFall()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private bool ShouldGoUp()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    private bool ShouldGoDown()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    private bool ShouldGoLeft()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }

    private bool ShouldGoRight()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    //void Move()
    //{
    //    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.Translate(0.1f, 0f, 0f);
    //    }
    //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.Translate(-0.1f, 0f, 0f);
    //    }
    //    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.Translate(0.0f, -0.1f, 0f);
    //    }
    //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.Translate(0.0f, 0.1f, 0f);
    //    }
    //}

    //void Animate()
    //{
    //    dropImage.transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Cos(Time.time * 10) * 10);
    //}
}
