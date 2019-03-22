using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject transparentBackground;
    public GameObject instructionBoard;
    public GameObject instruction;
    public GameObject cross;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void startInfinity() 
    {
        PlayerPrefs.SetInt("gameMode", 1);
        SceneManager.LoadScene("CharacterSelection");
    }

    public void startMaze() 
    {
        PlayerPrefs.SetInt("gameMode", 2);
        SceneManager.LoadScene("CharacterSelection");
    }

    public void showInstruction()
    {
    	transparentBackground.SetActive(true);
        instructionBoard.SetActive(true);
        instruction.SetActive(true);
        cross.SetActive(true);
    }

    public void closeInstruction()
    {
        transparentBackground.SetActive(false);
        instructionBoard.SetActive(false);
        instruction.SetActive(false);
        cross.SetActive(false);
    }

    public void back()
    {
        SceneManager.LoadScene("Login");
    }
}
