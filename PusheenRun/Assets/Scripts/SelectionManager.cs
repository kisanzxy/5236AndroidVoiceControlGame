using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scrollForward() {
        CharacterSpriteRenderer.CharacterID++;
        int cur_id = CharacterSpriteRenderer.CharacterID % CharacterSpriteRenderer.CHARAC_TOTAL_NUM;
        CharacterSpriteRenderer.CharacterID = cur_id;
    }
    public void scrollBackward()
    {
        int cur_id;
        if (CharacterSpriteRenderer.CharacterID == 0)
        {
            cur_id = CharacterSpriteRenderer.CHARAC_TOTAL_NUM -1;
        }
        else
        {
            CharacterSpriteRenderer.CharacterID--;
            cur_id = CharacterSpriteRenderer.CharacterID % CharacterSpriteRenderer.CHARAC_TOTAL_NUM;
        }
        CharacterSpriteRenderer.CharacterID = cur_id;
    }
    public void selection() {
        if (PlayerPrefs.GetInt("gameMode") == 1){
            SceneManager.LoadScene("Level1");
        } else if (PlayerPrefs.GetInt("gameMode") == 2){
            SceneManager.LoadScene("Maze");
        }
        
    }
    public void back(){
        SceneManager.LoadScene("StartMenu");
    }
}
