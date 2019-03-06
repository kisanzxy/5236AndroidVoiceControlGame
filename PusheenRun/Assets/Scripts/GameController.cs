using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	private bool gameover;
    private bool hasAppeared;
    private bool paused;
    private float timer;
    public Text scoreText;
	public Text timerText;
    public GameObject gameoverText;             //A reference to the object that displays the text which appears when the player dies.
    public GameObject pauseBackground;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject volumeButton;
    public SpriteRenderer m_SpriteRenderer;
    public Image volumeButtonImage;
    public Sprite musicOn;
    public Sprite musicOff;

    private int score = 0;                      //The player's score.
    private int count = 0;
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
        PlayerPrefs.SetInt("PlayerScore", 0);
        //Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
        gameover = false;
        paused = false;
        Time.timeScale = 1;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        // //If the game is over and the player has pressed some input...
        // if (gameOver && Input.GetMouseButtonDown(0)) 
        // {
        //     //...reload the current scene.
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
        if(!paused){
            timer += Time.deltaTime;
        }
        if(m_SpriteRenderer.isVisible){
            hasAppeared = true;
        }
        if(hasAppeared){ 
            if(!m_SpriteRenderer.isVisible){
                characterDied();
            }
        }
        if (gameover) {
            PlayerPrefs.SetInt("PlayerScore", score);
        }
        int minutes = (int)(timer/60f);
        int seconds = (int)(timer % 60f);
        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        if(count == 100)
        {
            count = 0;
            updateScores();
        }
        

        //Debug.Log(timer);
    }

    public void updateScores()
    {
        if (gameover)   
            return;
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void characterDied()
    {
        paused = true;
        gameover = true;
        Time.timeScale = 0;
        pauseBackground.SetActive(true);
    	gameoverText.SetActive(true);
        exitButton.SetActive(true);
        restartButton.SetActive(true);
        volumeButton.SetActive(true);
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseBackground.SetActive(true);
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
        restartButton.SetActive(true);
        volumeButton.SetActive(true);
    }

    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseBackground.SetActive(false);
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        restartButton.SetActive(false);
        volumeButton.SetActive(false);
    }

    public void exit()
    {
        paused = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("PlayerScore", score);
        SceneManager.LoadScene("StartMenu");
    }

    public void restart()
    {   
        SceneManager.LoadScene("Level1");
    }
    public bool isGameOver()
    {
        return this.gameover;
    }

    public int getScore() {
        return this.score;
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

        //Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
    }
}
