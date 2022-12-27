
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float bullSpeed;
    //private float direction;
    public bool hit;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator anim;
    private float lifetime;
    PlayerMove2 playerMove2;
    public float en_damage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        circleCollider= GetComponent<CircleCollider2D>();
        rb= GetComponent<Rigidbody2D>();
        playerMove2 = GameObject.Find("Player").GetComponent<PlayerMove2>();
    }

    private void Update()
    {
        if (hit)
        {
            //print("that section in the update lol");
            return;
        }
            
        
        //float movementSpeed = bullSpeed * Time.deltaTime;
        //transform.Translate(movementSpeed, 0, 0);
        
        lifetime += Time.deltaTime;
        if (lifetime >1.2f) gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true; 
        circleCollider.enabled = false;
        anim.SetTrigger("Explode");
        //Debug.Log("colliding bullet");
        rb.velocity= new Vector2(rb.velocity.x/5,rb.velocity.y/5);
        if (collision.tag == "enemy")
        {
            collision.GetComponent<EnemyDeets>().DamageFrenemy(en_damage);
        }

    }

    public void NewShot () 
    { 
       
        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;
        lifetime = 0;

        if (playerMove2.transform.localScale.x > 0)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * bullSpeed;
            //print("new shot created");
        }

        if (playerMove2.transform.localScale.x < 0)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * -bullSpeed;
            //print("new shot created to the left");
        }

        //direction = _direction;
        //float facing = transform.localScale.x;
        //if (Mathf.Sign(facing) != _direction)
        {
            //facing = -facing;
        }

        //transform.localScale = new Vector3(facing, transform.localScale.y, transform.localScale.z);

    

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
