using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //global variables fue to static keyword
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool mute = false;
    public static bool gameIsPaused;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;


    public static int currentLevelIndex;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public Slider gameProgressSlider;

    public static int numberOfPassedRings;
    public static int score = 0; // static variables can persist through levels


    private void Awake()
    {
        //save current level on the device
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
       isGameStarted= gameOver =gameIsPaused = levelCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //update UI elements
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        //tap on the screen
       if((Input.touchCount >0 && Input.GetTouch(0).phase== TouchPhase.Began && !isGameStarted) // touch count is important for mobile
       ||(Input.GetMouseButtonDown(0) && !isGameStarted))
        {
            //check if we are touching or pressing UI elements
          //  if (EventSystem.current.IsPointerOverGameObject( Input.GetTouch(0).fingerId))
            //    return;


            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            Debug.Log("gameplay panel activated");
            startMenuPanel.SetActive(false);
  
            
        }

        if (gameOver)
        {
            //stop the game
            Time.timeScale = 0;
            //enable game over panel
            gameOverPanel.SetActive(true);
            //reload the level 
            if (Input.GetButtonDown("Fire1")) // works for both pc and mobile
            {
                if(score> PlayerPrefs.GetInt("HighScore", 0)) // update current high score
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }

                score = 0;
                SceneManager.LoadScene("Level");
            }
        }

        if (levelCompleted)
        {
            //enable level completed panel
            levelCompletedPanel.SetActive(true);
            //reload the level 
            if (Input.GetButtonDown("Fire1")) // works for both pc and mobile
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
       
    }
    public void PauseGame()
    {
        gameIsPaused = true;
        //if (gameIsPaused == true)
        
            Time.timeScale = 0f;
        
       /* else
        {
            Time.timeScale = 1;
        }*/
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
