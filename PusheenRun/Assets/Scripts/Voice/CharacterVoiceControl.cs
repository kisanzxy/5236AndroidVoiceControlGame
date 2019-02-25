using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVoiceControl : MonoBehaviour
{
	public float moveSpeed = 0;     // horizontal speed
    public float maxSpeed = 5f;
    public Text transcriptText; 
    // public Transform groundCheck;
    // public LayerMask whatIsGround;

    private SpriteRenderer m_SpriteRenderer;
    private Sprite m_CurSprite;         // sprite used for load image
    private bool isDead = false;    //Has the character fall out of the screen?
    
    private Animator m_Anim;          //Reference to the Animator component.
    private Rigidbody2D m_Character;  //Reference to the Rigidbody2D of the character.
    private BoxCollider2D m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CharacterVoiceControl Starts");
        //Load current sprite
        m_CurSprite = CharacterSpriteFactory.moveSpriteFactory(CharacterSpriteRenderer.CharacterID);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_CurSprite;
        //Get reference to the Animator component attached to the character.
        //m_Anim = GetComponent<Animator>();
        //Get reference to the Rigidbody2D component attached to the character.
        m_Character = GetComponent<Rigidbody2D>();
        Debug.Log(m_Character);
        //Get reference to the BoxCollider2D component attached to the character.
        // m_Collider = GetComponent<BoxCollider2D>();
        // Debug.Log(m_Collider);
        //set size
        // m_Collider.size = m_SpriteRenderer.size;
        // m_Collider.offset = new Vector2(1f, 2.6f);
        // m_Character.velocity = Vector2.right;
    }

	void FixedUpdate()
    {
		// grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

	void Update () {
    }

	public void Move(string direction)
	{
        transcriptText.text = "Action: " + direction;
		Debug.Log("Direction: " + direction);
		switch(direction){
			case "Right":
				m_Character.velocity = Vector2.right;
				break;

			// left
			case "Left":
				m_Character.velocity = Vector2.left;
				break;

			// up
			case "Up":
				m_Character.velocity = Vector2.up;
				break;

			//down
			case "Down":
				m_Character.velocity = Vector2.down;
				break;

			default:
				break;
		}
	}
}
