using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject instruction;
    public GameObject cross;
    public Image volumeButton;
    public Sprite musicOn;
    public Sprite musicOff;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("musicStatus")) {
            if(PlayerPrefs.GetInt("musicStatus") == 1){
                volumeButton.sprite = musicOn;
            } else {
                volumeButton.sprite = musicOff;
            }
        } else {
            PlayerPrefs.SetInt("musicStatus", 1);
        }
        
        Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() 
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void musicSetting() {
        if(PlayerPrefs.GetInt("musicStatus") == 1){
            PlayerPrefs.SetInt("musicStatus", 0);
            volumeButton.sprite = musicOff;
        } else {
            PlayerPrefs.SetInt("musicStatus", 1);
            volumeButton.sprite = musicOn;
        }

        Debug.Log(PlayerPrefs.GetInt("musicStatus").ToString());
    }

    public void showInstruction()
    {
        instruction.SetActive(true);
        cross.SetActive(true);
    }

    public void closeInstruction()
    {
        instruction.SetActive(false);
        cross.SetActive(false);
    }
}
