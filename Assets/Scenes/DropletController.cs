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
    public Material level3Material1;
    public Text storyText;
    public AudioSource gameOver;
    public AudioSource movement;
    public AudioSource audioHitByParasite;
    public AudioSource audioHitByWall;
    public AudioSource audioGameEnd1;
    public AudioSource audioGameEnd2;
    public AudioSource audioLevelEnd;
    public int score;

    private bool gamePaused = true;
    private bool gameFinished = false;
    private bool isShowingStory = true;
    private int currentLevel = 1;
    private float velX = 0.0f;
    private float velXDelta = 0.02f;
    private int nextNarrationTextIndex = 1;

    private static float VEL_X_MAX = 0.2f;

    private void Start()
    {
        movement.volume = 0.0f;
        movement.Play();
    }

    [System.Obsolete]
    private void Update()
    {
        // Hack: keys for debugging and switching between levels
        // Maybe disable in final product
        if (Input.GetKey(KeyCode.Alpha2))
        {
            PauseGame();
            UpdateLevel(2);
            nextNarrationTextIndex = 1;
            isShowingStory = true;
            ShowStory();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            PauseGame();
            UpdateLevel(3);
            nextNarrationTextIndex = 1;
            isShowingStory = true;
            ShowStory();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            PauseGame();
            UpdateLevel(4);
            nextNarrationTextIndex = 1;
            isShowingStory = true;
            ShowStory();
        }

        if (IsLevelCleared())
        {
            PauseGame();
            UpdateLevel(currentLevel + 1);
            nextNarrationTextIndex = 1;
            isShowingStory = true;
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
            // Reloading the whole scene resets all the objects to the right place.
            /*score = 0;
            nextNarrationTextIndex = 0;
            isShowingStory = true;
            gameFinished = false;
            gameOverScreen.SetActive(false);
            UpdateLevel(1);*/
            SceneManager.LoadScene(0);
        }

        if (isShowingStory)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                nextNarrationTextIndex += 1;
                //Debug.Log("Narration Index Inside: " + nextNarrationTextIndex);
            }

            if (!narrationArea.active)
            {
                narrationArea.SetActive(true);
            }

            PauseGame();
            ShowStory();

            //Debug.Log("Narration Index: " + nextNarrationTextIndex);

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

    private void Die()
    {
        gameOverScreen.SetActive(true);
        gameOver.PlayDelayed(1);
        gameFinished = true;
        PauseGame();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameFinished) return;
        if (other.name.StartsWith("Enemy"))
        {
            audioHitByParasite.Play();
            Die();
        }
    }

    public void OnHitWall()
    {
        if (gameFinished) return;
        audioHitByWall.Play();
        Die();
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
        if (ResistFall())
        {
            movement.volume = Mathf.Clamp(movement.volume + 1.0f * Time.deltaTime, 0.0f, 1.0f);
        }
        else
        {
            movement.volume = Mathf.Clamp(movement.volume - 3.0f * Time.deltaTime, 0.0f, 1.0f);
        }

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

    private bool IsStoryFinished()
    {
        if (currentLevel == 1)
        {
            return nextNarrationTextIndex > 3;
        }
        else if (currentLevel == 2)
        {
            return nextNarrationTextIndex > 1;
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
        if (level <= 3)
        {
            audioLevelEnd.Play();
        }
        else
        {
            audioGameEnd1.Play();
            audioGameEnd2.PlayDelayed(1);
        }
        GameObject.Find("MonsterSpawner").
        GetComponent<MonsterSpawner>().currentLevel = level;
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
            background2.enabled = false;
            background3.enabled = false;
        }
        else if (level == 3)
        {
            background1.material = level3Material1;
            background2.enabled = false;
            background3.enabled = false;
        }
    }

    private void ShowStory()
    {
        if (currentLevel == 1)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "Welcome little water drop!\n" +
                    "Thank you for answering the call of this tree.\n\n" +
                    "Press ENTER to continue.";
            }
            else if (nextNarrationTextIndex == 2)
            {
                storyText.text = "You find yourself underground in the roots where you start your journey.\n" +
                    "You are feeling a force pulling you upward - follow it!\n\n" +
                    "Press ENTER to continue.";
            }
            else if (nextNarrationTextIndex == 3)
            {
                storyText.text = "But wait - something's not right. The tree is being attacked by parasites. Don’t let them get you!\n\n" +
                    "Hold SPACE to move around.\n" +
                    "Press ENTER to continue.";
            }
        }
        else if (currentLevel == 2)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "That was close!\n" +
                    "You’ve reached the trunk! You notice that this part of the tree is more sick. Be careful!\n\n" +
                    "Press ENTER to continue.";
            }
        }
        else if (currentLevel == 3)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "You’re almost to a leaf! Keep going and keep avoiding those parasites!\n\n" +
                    "Press ENTER to continue.";
            }
        }
        else if (currentLevel == 4)
        {
            if (nextNarrationTextIndex == 1)
            {
                storyText.text = "You’ve done it! You’ve reached the top branch of the tree and found a cozy leaf\n" +
                   "A different kind of force is pulling you up again and you feel a tingling sensation. Slowly, you change into vapor for your next journey.";
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
