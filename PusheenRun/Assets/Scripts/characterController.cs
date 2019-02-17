using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float upForce = 200f;    //Upward force for jumping
    public float moveSpeed = 0;     // horizontal speed
    public float maxSpeed = 5f;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private SpriteRenderer m_SpriteRenderer;
    private Sprite m_CurSprite;         // sprite used for load image
    private bool isDead = false;    //Has the character fall out of the screen?
    
    private Animator m_Anim;          //Reference to the Animator component.
    private Rigidbody2D m_Character;  //Reference to the Rigidbody2D of the character.
    private BoxCollider2D m_Collider;
    //private float m_ScaleX, m_ScaleY, m_ScaleZ;


    bool facingRight = true;
    bool grounded = false;
    float groundRadius = 1.5f;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        //Load current sprite
        m_CurSprite = CharacterSpriteFactory.moveSpriteFactory(CharacterSpriteRenderer.CharacterID);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_CurSprite;
        //Get reference to the Animator component attached to the character.
        //m_Anim = GetComponent<Animator>();
        //Get reference to the Rigidbody2D component attached to the character.
        m_Character = GetComponent<Rigidbody2D>();
        //Get reference to the BoxCollider2D component attached to the character.
        m_Collider = GetComponent<BoxCollider2D>();
        //set size
        m_Collider.size = m_SpriteRenderer.size;
        m_Collider.offset = new Vector2(1f, 2.6f);

    }

    // Update is called once per frame

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

    }
    void Update()
    {

       // Debug.Log(grounded);
        if (grounded)
            m_Character.velocity = new Vector2(moveSpeed, m_Character.velocity.y);
       // Debug.Log(m_Character.velocity.x);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            m_Character.velocity = new Vector2(m_Character.velocity.x, upForce);
        }
    }


    void Flip()
    {
    	 facingRight = !facingRight;
    	 Vector3 theScale = transform.localScale;
    	 theScale.x *= -1;
    	 transform.localScale = theScale;
    }
}
