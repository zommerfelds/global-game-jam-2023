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
    private int nextNarrationTextIndex = 1;

    private static float VEL_X_MAX = 0.2f;

    private void Update()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        }

        if (RestartGame())
        {
            score = 0;
            nextNarrationTextIndex += 1;
            narrationArea.SetActive(true);
            gameOverScreen.SetActive(false);

            ShowLevel1Story();
        }

        if (nextNarrationTextIndex > 3)
        {
            Time.timeScale = 1;
            gamePaused = false;
            narrationArea.SetActive(false);
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

            nextNarrationTextIndex = 0;
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

        //Vector3 newPos = transform.position + new Vector3(velX, 0, 0);
        //transform.position = Vector3.Lerp(
        //    transform.position,
        //    newPos,
        //    Time.deltaTime * 50);
        transform.Translate(velX, 0f, 0f);
    }

    private void Animate()
    {
        //dropImage.transform.rotation = Quaternion.Lerp(
        //    dropImage.transform.rotation,
        //    Quaternion.Euler(0, 0, 0 - velX / VEL_X_MAX * 30 // face in the direction of speed
        //    + Mathf.Cos(Time.time * 50) * 5 // wiggle a bit
        //), Time.deltaTime * 10);

        dropImage.transform.rotation = Quaternion.Euler(0, 0, 0
            - velX / VEL_X_MAX * 30 // face in the direction of speed
            + Mathf.Cos(Time.time * 30) * 5 // wiggle a bit
        );
    }

    private void ShowLevel1Story()
    {
        Debug.Log("Story Page: " + nextNarrationTextIndex);
        if (nextNarrationTextIndex == 1)
        {
            storyText.text = "Lorem Ipsum is simply dummy text of the "+
                "printing and typesetting industry. Lorem Ipsum has been the " +
                "industry's standard dummy text ever since the 1500s, when " +
                "an unknown printer took a galley of type and scrambled it " +
                "to make a type specimen book. It has survived not only five " +
                "centuries, but also the leap into electronic typesetting, " +
                "remaining essentially unchanged.";
        }
        else if (nextNarrationTextIndex == 2)
        {
            storyText.text = "Put simply, changing the time scale from its " +
                "default of one will speed up or slow the game down ? for " +
                "example, you can run the game at half speed with a time " +
                "scale of 0.5, or twice as fast with a timescale of 2). " +
                "Setting it to zero, pauses the game entirely.";
        }
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
