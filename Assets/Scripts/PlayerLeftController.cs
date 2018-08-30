using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    // this creates the littel drop down in the unity inspector 
    public enum Facing { Left, Right };
    public Facing orientation;
    [HideInInspector] public bool jump = true;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    //public Transform groundCheck;

    public LayerMask groundLayer;

    private bool isFalling = false;
    private bool isDropping = false;
    private bool grounded = false;

    private Animator animator;
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;

	// Use this for initialization
	void Start () {
	}

    private void Awake()
    {
        //animator = GetComponene<Animator>(); 
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        //bc2D.enabled = false;
        
    }

    // Update is called once per frame
    void Update () {

        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //if (Input.GetKeyDown(KeyCode.W) && grounded )
        // if the W key is pressed
        //if (Input.GetKeyDown(KeyCode.W) && IsGrounded() )
        if (Input.GetKeyDown(KeyCode.W) )
        {
            jump = true;    
        }

        // if the down key is pressed for the player, (Player Left, 'S' Key)
        // then have the player fall through the platform. 
        // this will involve them ignoring the edge collider until they're out of the trigger zone
        //if (Input.GetKeyDown(KeyCode.S) && IsGrounded() )
        if (Input.GetKeyDown(KeyCode.S) )
        {
            Debug.Log("S");
            isDropping = true;
        } 

	}

    private void FixedUpdate()
    {

        /**
        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));
        if (h * rb2D.velocity.x < maxSpeed)
        {
            rb2D.AddForce(Vector2.right * h * moveForce);
        }
        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
        {
            rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
        }
        if ( h > 0 && !facingRight)
        {
            Flip();
        } else if (h < 0 && facingRight)
        {
            Flip();
        }
        **/
        if (jump)
        {
            //animation.SetTrigger("Jump");
            rb2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        Debug.Log("OnCollisionEnter");
        if (isDropping)
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /**
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision);
        Debug.Log("OnCollisionExit");
        isDropping = false;
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    **/

    // this is a handy dandy method for flippin a sprite along it's x-axis
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }   

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;           // shorthand for Vector2(0, -1.0f)
        float distance = 1.0f;                      // we don't want the raycast to go on forever, 
                                                    // just to check the ground beneath them

        // This lets you actually view the RayCast, useful for Debugging and tweaking
        Debug.DrawRay(position, direction, Color.green);
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
