using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropletController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dropImage;
    public GameObject narrationArea;
    public Renderer background1;
    public Renderer background2;
    public Renderer background3;
    public Material level1Material1;
    public Material level2Material1;
    public Material level1Material2;
    public Material level1Material3;
    public Material level2Material2;
    public Material level2Material3;
    public Text storyText;
    public AudioSource gameOver;
    public AudioSource movement;
    public int score;

    private bool gamePaused = true;
    private bool gameFinished = false;
    private bool isShowingStory = true;
    private int currentLevel = 1;
    private float velX = 0.0f;
    private float velXDelta = 0.02f;
    private int nextNarrationTextIndex = 1;

    private static float VEL_X_MAX = 0.2f;

    [System.Obsolete]
    private void Update()
    {
        if (IsLevelCleared())
        {
            Debug.Log("Level Cleared: " + score);
            PauseGame();
            UpdateLevel(currentLevel + 1);
            nextNarrationTextIndex = 0;
            isShowingStory = true;
            storyText.text = "Yay!!! Hurray, the level is cleared.";
        }

        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (IsRestartOfGameRequested())
        {
            score = 0;
            nextNarrationTextIndex = 0;
            isShowingStory = true;
            gameFinished = false;
            gameOverScreen.SetActive(false);
            UpdateLevel(1);
        }

        if (isShowingStory)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                nextNarrationTextIndex += 1;
                Debug.Log("Narration Index Inside: " + nextNarrationTextIndex);
            }

            if (!narrationArea.active)
            {
                narrationArea.SetActive(true);
            }

            PauseGame();
            ShowStory();

            Debug.Log("Narration Index: " + nextNarrationTextIndex);

            if (IsStoryFinished())
            {
                isShowingStory = false;
                gamePaused = false;
                if (narrationArea.active)
                {
                    narrationArea.SetActive(false);
                }
            }
        }
    }

    [System.Obsolete]
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
        if (other.name.StartsWith("Enemy"))
        {
            gameOverScreen.SetActive(true);
            gameOver.Play();
            gameFinished = true;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        if (!gamePaused)
        {
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
        movement.Play();
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

    private bool IsStoryFinished()
    {
        if (currentLevel == 1)
        {
            return nextNarrationTextIndex > 2;
        }
        else if (currentLevel == 2)
        {
            return nextNarrationTextIndex > 3;
        }
        else if (currentLevel == 3)
        {
            return nextNarrationTextIndex > 1;
        }
        else if (currentLevel == 4)
        {
            return nextNarrationTextIndex > 1;
        }

        return false;
    }

    private bool IsLevelCleared()
    {
        bool isLevelCleared = false;
        if (currentLevel == 1)
        {
            isLevelCleared = score > 3000;
        }
        else if (currentLevel == 2)
        {
            isLevelCleared = score > 7000;
        }
        else if (currentLevel == 3)
        {
            isLevelCleared = score > 12000;
        }

        return isLevelCleared;
    }

    private void UpdateLevel(int level)
    {
        currentLevel = level;
        if (level == 1)
        {
            background1.material = level1Material1;
            background2.material = level1Material2;
            background3.material = level1Material3;
        }
        else if (level == 2)
        {
            background1.material = level2Material1;
            // background2.material = level2Material2;
            // background3.material = level2Material3;
        }
        else if (level == 3)
        {
            // Update level3 textures here.
        }
    }

    private void ShowStory()
    {
        if (currentLevel == 1)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "Lorem Ipsum is simply dummy text of the " +
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
        else if (currentLevel == 2)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "Hibernian head coach Lee Johnson tells BBC " +
                    "Sportsound: Obviously, I've known for a few weeks now, " +
                    "and I have to say Ron [Gordon] has shown unbelievable " +
                    "strength and charisma. We've gone through a sticky " +
                    "patch but what he's dealt with, what his family has " +
                    "dealt with, we've got every sympathy for him. We wish " +
                    "him the quickest recovery possible.";
            }
            else if (nextNarrationTextIndex == 2)
            {
                storyText.text = "You just sense things might be turning for " +
                    "Malky McKay and Ross County. A win and a draw in their " +
                    "past two games and just one defeat in their past five " +
                    "league matches. However, since sharing six goals in " +
                    "Dingwall in a thriller in January of last year, County " +
                    "have lost their past three encounters with Rangers. " +
                    "Aggregate score? 9 - 1.";
            }
            else if (nextNarrationTextIndex == 3)
            {
                storyText.text = "Press 1 to move further in trunk or 2 " +
                    "to move towards a branch for photosynthesis.";
            }
        }
        else if (currentLevel == 3)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "This is the last level of the game.";
            }
        }
        else if (currentLevel == 4)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "This is the end of the game, Well done!!!";
            }
        }
    }

    private bool IsRestartOfGameRequested()
    {
        return gamePaused && gameFinished && Input.GetKey(KeyCode.Return);
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
