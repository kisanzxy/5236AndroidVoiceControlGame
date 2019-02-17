using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteRenderer : MonoBehaviour
{
    public static int CharacterID;
    private SpriteRenderer m_SpriteRenderer;
    private Sprite m_CurSprite;
    public  const int CHARAC_TOTAL_NUM = 4;

    // Start is called before the first frame update
    void Start()
    {
        CharacterID = 0;
        m_CurSprite = CharacterSpriteFactory.spriteFactory(CharacterID);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        m_SpriteRenderer.sprite = m_CurSprite;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CharacterID);
        m_CurSprite = CharacterSpriteFactory.spriteFactory(CharacterID);
        m_SpriteRenderer.sprite = m_CurSprite;
    }


}
