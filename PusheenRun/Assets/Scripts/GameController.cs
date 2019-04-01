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
    private bool hasAppeared = false;

    public Text scoreText;
    public Text gameoverText;
    public GameObject gameoverTextObject;
    public Text finalScoreText;
    public GameObject finalScoreTextObject;
    public GameObject pauseBackground;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject showInstructionButton;
    public GameObject instructionBoard;
    public GameObject instruction;
    public GameObject cross;

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
        PlayerPrefs.SetInt("PlayerScore", 0);
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Maze"){
            timer = 301;
            boundaries = mazeGenerator.getBoundaries();
            // Debug.Log("leftBoundary: " + boundaries["left"]);
            // Debug.Log("topBoundary: " + boundaries["top"]);
        }
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_SpriteRenderer.isVisible){
            hasAppeared = true;
        }

        if (currentScene == "Level1"){
            if(!paused){
                timer += Time.deltaTime;
                score = (int) timer;
            }
            if(hasAppeared){ 
                if (m_Character.transform.position.y < -6f){
                // if(!m_SpriteRenderer.isVisible){
                    PlayerPrefs.SetInt("PlayerScore", score);
                    finalScoreText.text = scoreText.text;
                    characterDied();
                }
            }
            scoreText.text = "Score: " + score.ToString();

        } else if (currentScene == "Maze"){
            if(!paused){
                timer -= Time.deltaTime;
                score = (int) timer;
            }
            if ((m_Character.transform.position.x <= boundaries["left"]) || 
                (m_Character.transform.position.x >= boundaries["right"]) ||
                (m_Character.transform.position.y <= boundaries["bottom"]) ||
                (m_Character.transform.position.y >= boundaries["top"])){
                gameoverText.text = "CONGRATULATIONS";
                characterDied();
            }
            if (timer <= 0){
                gameoverText.text = "GAMEOVER";
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
        finalScoreTextObject.SetActive(true);
    	gameoverTextObject.SetActive(true);
        exitButton.SetActive(true);
        if (currentScene == "Maze"){
            showInstructionButton.SetActive(true);
        }
        restartButton.SetActive(true);
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseBackground.SetActive(true);
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
        if (currentScene == "Maze"){
            showInstructionButton.SetActive(true);
        }
        restartButton.SetActive(true);
    }

    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseBackground.SetActive(false);
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        if (currentScene == "Maze"){
            showInstructionButton.SetActive(false);
        }
        restartButton.SetActive(false);
    }

    public void exit()
    {
        paused = false;
        Time.timeScale = 1;
        // PlayerPrefs.SetInt("PlayerScore", score);
        SceneManager.LoadScene("StartMenu");
    }

    public void restart()
    {   
        SceneManager.LoadScene(currentScene);
    }

    public void showInstruction()
    {
        // pauseBackground.SetActive(true);
        instructionBoard.SetActive(true);
        instruction.SetActive(true);
        cross.SetActive(true);
    }

    public void closeInstruction()
    {
        // pauseBackground.SetActive(false);
        instructionBoard.SetActive(false);
        instruction.SetActive(false);
        cross.SetActive(false);
    }

    public bool isGameOver()
    {
        return this.gameover;
    }

    public int getScore() {
        return this.score;
    }

    public string getScence(){
        return this.currentScene;
    }

}
