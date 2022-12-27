using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove2 : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    //[SerializeField] private float baseSpeed;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask WallsLayer;

    [SerializeField] private AudioSource waddle;
    [SerializeField] private AudioSource scream;
    [SerializeField] private AudioSource stomp;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    public Joystick joystick;
    touchTesting touchScript;
    //private float speedDif;
    //private float accelmove;
    public float frictionAmount;
    [SerializeField] private float accelrate;

    //awake is called before start, and unlike start- will be called even if the script component is disabled!
    private void Awake()
    {
        //These lines grab references for rigidbody and animator from object 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        touchScript = GameObject.Find("weapon").GetComponent<touchTesting>();
    }

    public void FixedUpdate()
    {
        //accelerated movement script
        //speedDif = maxSpeed - Mathf.Abs(body.velocity.x);
        //accelmove = speedDif * accelrate;

        //legacy accel
        if (joystick.Horizontal >= .2f && body.velocity.x <maxSpeed && !isGrounded() && !isGroundedW())
        {
            body.AddForce(new Vector2((accelrate*joystick.Horizontal)/2.4f, 0));
        }

        if (joystick.Horizontal <= -.2f && Mathf.Abs(body.velocity.x) < maxSpeed && !isGrounded() && !isGroundedW())
        {
            body.AddForce(new Vector2((accelrate*joystick.Horizontal)/2.4f, 0)); 
        }


        if (joystick.Horizontal >= .2f && body.velocity.x < maxSpeed && (isGroundedW() || isGrounded()))
        {
            body.AddForce(new Vector2(accelrate*joystick.Horizontal, 0));
        }

        if (joystick.Horizontal <= -.2f && Mathf.Abs(body.velocity.x) < maxSpeed && (isGroundedW() || isGrounded()))
        {
            body.AddForce(new Vector2(accelrate*joystick.Horizontal, 0));
        }

        //new accel
        //if (joystick.Horizontal >= .2f && body.velocity.x <maxSpeed)
        {
          //  body.AddForce(new Vector2(accelmove, 0));
        }

        //if (joystick.Horizontal <= -.2f && Mathf.Abs(body.velocity.x)< maxSpeed)
        {
          //  body.AddForce(new Vector2(-accelmove, 0)); 
        }

        //if (isGroundedW() && joystick.Horizontal >= .2f && body.velocity.x < maxSpeed)
        {
          //  body.AddForce(new Vector2(accelmove, 0));
        }

        //if (isGroundedW() && joystick.Horizontal <= -.2f && Mathf.Abs(body.velocity.x) < maxSpeed)
        {
          //  body.AddForce(new Vector2(-accelmove, 0));
        }


        //artificial friction
        if ((Mathf.Abs(joystick.Horizontal) < .18f) && (isGrounded() || isGroundedW())) 
        {
            float amount = Mathf.Min(Mathf.Abs(body.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(body.velocity.x);
            body.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
       

    }
    public void Update()
    {
        //accept joystick input
        //if (joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f)
        {
            //waddle.Play();
        }

        // Flip player when moving left-right 
        if (joystick.Horizontal > 0.001f)
            transform.localScale = Vector3.one;
        if (joystick.Horizontal < -0.001f)
            transform.localScale = new Vector3(-1, 1, 1);
            
        
        //flip player back upright when falling straight down from gflip 
        if (joystick.Horizontal == 0 && body.gravityScale > 0 && transform.localScale.x != (-1))
            transform.localScale = Vector3.one;
        if (joystick.Horizontal == 0 && body.gravityScale >0 && transform.localScale.x == -1)
            transform.localScale= new Vector3(-1,1,1);  

        //set animator parameters
        anim.SetBool("Run", joystick.Horizontal > .195f || joystick.Horizontal < -.195f);
        anim.SetBool("Grounded", isGrounded() || isGroundedW());
        anim.SetBool("WallJ", onWall());

        //legacy speed controller  
        //body.velocity = new Vector2(j.horizontal* baseSpeed, body.velocity.y);
        
        //jump is here 

        if (joystick.Vertical > .25f)
        {
            Jump();
        }
       // if (!isGrounded () && !isGroundedW())
          //  body.velocity = new Vector2(body.velocity.x+joystick.Horizontal, body.velocity.y);



        //wall jump stick
        if (wallJumpCooldown > 0.1f)
        {
            if ((onWall() || onWallB()) && !isGrounded() && !isGroundedW())
            {
                body.gravityScale = 1.5f;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;

        }
        else 
            wallJumpCooldown += Time.deltaTime;

        //Gravity flip logic 
        if (joystick.Vertical < -.4f)
            Gflip();
        if (body.gravityScale < -.01 && joystick.Horizontal== 0 && transform.localScale.x !=-1)
            transform.localScale = new Vector3(1,-1,1);
        if (body.gravityScale < -.01 && joystick.Horizontal == 0 && transform.localScale.x == -1)
            transform.localScale = new Vector3(-1, -1, 1);
        if (joystick.Horizontal >= 0.01f && body.gravityScale < -0.01f)
            transform.localScale = new Vector3(1, -1, 1);
        else if (joystick.Horizontal < -0.001f && body.gravityScale < -0.01f)
            transform.localScale = new Vector3(-1, -1, 1);

        touchScript.joyTouch = -1;

        //multi touch logic
        if (Input.touchCount > 0)
        {
            if (Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical) > 0)
            {
                touchScript.joyTouch = Input.GetTouch(0).fingerId;
            }
            else 
                touchScript.joyTouch = -1;
        }
    }

    private void Jump()
    {
        //jump when standing on "ground"
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            anim.SetTrigger("Jump");
        }
        //jump when standing on top of "walls"
        if (isGroundedW())
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            anim.SetTrigger("Jump");
        }

        //Wall jump 
        else if (onWall() && !isGrounded() && !isGroundedW())
        {
            if (joystick.Horizontal <.2 )
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8.1f, jump * 1.15f);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8.1f, jump * 1.15f);

            wallJumpCooldown = 0;
        }
        //wall jump when wall behind
        else if (onWallB() && !isGrounded() && !isGroundedW())
        {
            if (joystick.Horizontal < .2)
            {
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 7.5f, jump * 1.1f);
                transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 7, jump * 1.1f);

            wallJumpCooldown = 0;
        }

        //Double jump 
        //if (Input.GetButtonUp("Jump") && body.velocity.y >0f)
        //{
        //body.velocity = new Vector2(body.velocity.x, body.velocity.y * .6f);
        //anim.SetTrigger("dJump");
        //}

    }

   

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, (groundLayer));
        return raycastHit.collider != null;
    }

    private bool isGroundedW()
    {
        RaycastHit2D raycastHitt = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, (WallsLayer));
        return raycastHitt.collider != null;
    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.25f, WallsLayer);
        return raycastHit.collider != null;
    }

    private bool onWallB()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-transform.localScale.x, 0), 0.25f, WallsLayer);
        return raycastHit.collider != null;
    }

    private void Gflip()
    {
        body.gravityScale = -1.9f;        
    }

    
  
        
   
}
