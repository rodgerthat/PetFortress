using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = true;
    [HideInInspector] public bool drop = false;

    // this creates the littel drop down in the unity inspector 
    public enum Facing { Left, Right };
    public Facing orientation;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    //public Transform groundCheck;

    public LayerMask groundLayer;

    private bool grounded = false;

    private Animator animator;
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;

    // Bullet
    public GameObject bullet;
    Vector2 weaponPosition;
    Vector2 localWeaponPosition;
    public float fireRate = 0.5f;
    private float nextFire = 0;

    // Use this for initialization
    void Start () {
	}

    private void Awake()
    {
        //animator = GetComponene<Animator>(); 
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        //bc2D.enabled = false;

        // get the weapon's location
        //weaponPosition = gameObject.transform.GetChild(0).transform.localPosition;  // whew! this gets the first index child
        localWeaponPosition = gameObject.transform.Find("Weapon").transform.localPosition;
        //localWeaponPositionOffset = transform.localPosition - localWeaponPosition;       

    }

    // Update is called once per frame
    void Update () {

        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //if (Input.GetKeyDown(KeyCode.W) && grounded )
        // if the W key is pressed
        //if (Input.GetKeyDown(KeyCode.W) && IsGrounded() )
        if (Input.GetKeyDown(KeyCode.UpArrow) )
        {
            jump = true;    
        }

        // if the down key is pressed for the player, (Player Left, 'S' Key)
        // then have the player fall through the platform. 
        // this will involve them ignoring the edge collider until they're out of the trigger zone
        //if (Input.GetKeyDown(KeyCode.S) && IsGrounded() )
        if (Input.GetKey(KeyCode.DownArrow) )
        {
            drop = true;
        } else
        {
            drop = false;
        }


        // Firing the Weapon
        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireWeapon();
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

        // might not need to do this here. Better in update()?
        if (drop)
        {
            gameObject.layer = 9;   // 9 is the freefall layer
        }
        else
        {
            gameObject.layer = 8;   // 8 is the platforms layer
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    }

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

    void FireWeapon()
    {
        Debug.Log(weaponPosition);
        Debug.Log(localWeaponPosition);
        weaponPosition = transform.position;
        //weaponPosition += localWeaponPosition;
        // trying to get the value of this dynamically based on the position of the child gameObject "Weapon"
        weaponPosition += new Vector2(-0.8f, 0.1f);
        Instantiate(bullet, weaponPosition, Quaternion.identity);
    }

}
