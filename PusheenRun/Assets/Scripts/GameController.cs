using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	private bool gameover = false;
    private bool paused;
    private float timer;
    public Text scoreText;
	public Text timerText;
    public GameObject gameoverText;             //A reference to the object that displays the text which appears when the player dies.
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject volumeButton;
    public Image volumeButtonImage;
    public Sprite musicOn;
    public Sprite musicOff;

    private int score = 0;                      //The player's score.
    public float scrollSpeed = -1.5f;

    // Initialize game
    void Awake()
    {
        if (instance == null)
        {
        	instance = this;
        }
        else if (instance != this)
        {
        	Destroy(gameObject);
        }
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("musicStatus") == 1){
            volumeButtonImage.sprite = musicOn;
        } else {
            volumeButtonImage.sprite = musicOff;
        }

        Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());

        paused = false;
        Time.timeScale = 1;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // //If the game is over and the player has pressed some input...
        // if (gameOver && Input.GetMouseButtonDown(0)) 
        // {
        //     //...reload the current scene.
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
        if(!paused){
            timer += Time.deltaTime;
        }
        int minutes = (int)(timer/60f);
        int seconds = (int)(timer % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void updateScores()
    {
        if (gameover)   
            return;
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void CharacterDied()
    {
    	gameoverText.SetActive(true);
    	gameover = true;
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
        restartButton.SetActive(true);
        volumeButton.SetActive(true);
    }

    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        volumeButton.SetActive(false);
    }

    public void exit()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void restart()
    {   
        SceneManager.LoadScene("Level1");
    }

    public void musicSetting() 
    {
        if(PlayerPrefs.GetInt("musicStatus") == 1){
            PlayerPrefs.SetInt("musicStatus", 0);
            volumeButtonImage.sprite = musicOff;
        } else {
            PlayerPrefs.SetInt("musicStatus", 1);
            volumeButtonImage.sprite = musicOn;
        }

        Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
    }
}
