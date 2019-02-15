using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float upForce = 200f;    //Upward force for jumping
    private bool isDead = false;    //Has the character fall out of the screen?
    
    private Animator anim;          //Reference to the Animator component.
    private Rigidbody2D character;  //Reference to the Rigidbody2D of the character.


	public float maxSpeed = 5f;
	bool facingRight = true;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to the Animator component attached to the character.
        anim = GetComponent<Animator>();
        //Get reference to the Rigidbody2D component attached to the character.
        character = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        if(grounded)
            doubleJump = false;

        anim.SetFloat("vSpeed", character.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        character.velocity = new Vector2(move * maxSpeed, character.velocity.y);
        if (move > 0 && !facingRight)
        	Flip();
        else if (move < 0 && facingRight)
        	Flip();
    }

    void Update()
    {
        if (isDead == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //Zero out the character's current y velocity
                character.velocity = Vector2.zero;
                //Give the character some upward force.
                character.AddForce(new Vector2(0, upForce));
            }
        }
        if((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            character.velocity = Vector2.zero;
            character.AddForce(new Vector2(0, upForce));

            if (!doubleJump && !grounded)
                doubleJump = true;
        }
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     character.velocity = Vector2.zero;
    //     // If the bird collides with something set it to dead...
    //     isDead = true;
    //     //...tell the Animator about it...
    //     anim.SetTrigger ("Die");
    //     //...and tell the game control about it.
    //     // GameController.instance.CharacterDied();
    // }

    void Flip()
    {
    	 facingRight = !facingRight;
    	 Vector3 theScale = transform.localScale;
    	 theScale.x *= -1;
    	 transform.localScale = theScale;
    }
}
