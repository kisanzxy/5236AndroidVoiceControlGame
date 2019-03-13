using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;
    private string currentScene;
	private bool gameover = false;
    private bool paused = false;
    private float timer = 0;
    private bool hasAppeared;

    public Text scoreText;
    public GameObject gameoverText;
    public GameObject pauseBackground;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject volumeButton;
    public Image volumeButtonImage;
    public Sprite musicOn;
    public Sprite musicOff;

    public SpriteRenderer m_SpriteRenderer;
    public GameObject m_Character;

    public MazeGenerator mazeGenerator;
    private Dictionary<string, float> boundaries;
    private int score = 0;

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
        currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
        if (currentScene == "Maze"){
            boundaries = mazeGenerator.getBoundaries();
            Debug.Log("leftBoundary: " + boundaries["left"]);
            Debug.Log("topBoundary: " + boundaries["top"]);
        }
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused){
            timer += Time.deltaTime;
            score = (int) timer;
        }

        if(m_SpriteRenderer.isVisible){
            hasAppeared = true;
        }

        if (currentScene == "Level1"){
            if(hasAppeared){ 
                if(!m_SpriteRenderer.isVisible){
                    characterDied();
                }
            }
            scoreText.text = "Score: " + score.ToString();
            if (gameover) {
                PlayerPrefs.SetInt("PlayerScore", score);
            }

        } else if (currentScene == "Maze"){
            if ((m_Character.transform.position.x <= boundaries["left"]) || 
                (m_Character.transform.position.x >= boundaries["right"]) ||
                (m_Character.transform.position.y <= boundaries["bottom"]) ||
                (m_Character.transform.position.y >= boundaries["top"])){
                
                characterDied();
            }
            int minutes = (int)(timer / 60f);
            int seconds = (int)(timer % 60f);
            scoreText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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

        Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
    }
}
