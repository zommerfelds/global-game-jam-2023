using UnityEngine;
using UnityEngine.SceneManagement;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dropImage;
    public int score = 0;
    public bool gameInProgress = true;

    private float velX = 0.0f;
    private float velXDelta = 0.02f;
    private int updatesSinceLastChange = 0;

    private static float VEL_X_MAX = 0.2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;

            score = 0;
            gameInProgress = true;
        }
    }

    private void LateUpdate()
    {
        if (gameInProgress)
        {
            score += 1;
        }
    }

    void FixedUpdate()
    {
        Move();
        Animate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched something: " + other.name);
        if (other.name.StartsWith("Enemy"))
        {
            gameOverScreen.SetActive(true);
            gameInProgress = false;
            Time.timeScale = 0;
        }
    }

    void Move()
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

    void Animate()
    {
        dropImage.transform.rotation = Quaternion.Euler(0, 0, 0
            - velX / VEL_X_MAX * 30 // face in the direction of speed
            + Mathf.Cos(Time.time * 30) * 5 // wiggle a bit
        );
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
