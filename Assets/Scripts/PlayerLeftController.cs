using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = true;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

    private bool grounded = false;
    private Animator animator;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
	}

    private void Awake()
    {
        //animator = GetComponene<Animator>(); 
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // if the W key is pressed
        if (Input.GetKeyDown(KeyCode.W) && grounded )
        {
            jump = true;    
        }
	}

    private void FixedUpdate()
    {
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
        if (jump)
        {
            //animation.SetTrigger("Jump");
            rb2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
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
