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
        SceneManager.LoadScene("Level1");
    }
}
