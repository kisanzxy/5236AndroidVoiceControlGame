using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public bool gameOver = false;
	public Text scoreText;                      //A reference to the UI text component that displays the player's score.
    public GameObject gameOvertext;             //A reference to the object that displays the text which appears when the player dies.

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

    // Update is called once per frame
    void Update()
    {
        // //If the game is over and the player has pressed some input...
        // if (gameOver && Input.GetMouseButtonDown(0)) 
        // {
        //     //...reload the current scene.
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
    }

    public void updateScores()
    {
    	//The bird can't score if the game is over.
        if (gameOver)   
            return;
        //If the game is not over, increase the score...
        score++;
        //...and adjust the score text.
        scoreText.text = "Score: " + score.ToString();
    }

    public void CharacterDied()
    {
    	gameOvertext.SetActive(true);
    	gameOver = true;
    }
}
