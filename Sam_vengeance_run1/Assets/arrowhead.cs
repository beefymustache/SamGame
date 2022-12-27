using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowhead : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    PlayerMove2 playerMove2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove2 = GameObject.Find("Player").GetComponent<PlayerMove2>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hasHit == false && playerMove2.transform.localScale.x == 1)
        {
            //calculates angle in radians, then convert to degree
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            //modify rotation of arrow using angle calculated above (angle, axis)
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (hasHit == false && playerMove2.transform.localScale.x == -1)
        {


            //calculates angle in radians, then convert to degree
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            //modify rotation of arrow using angle calculated above (angle, axis)
            transform.rotation = Quaternion.AngleAxis(-angle, -Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   //called everytime our arrow hits an object with a collider 
        hasHit= true;
        
       


        //bounces off of enemy 
        if (collision.gameObject.tag == ("enemy"))
        {
            // arrows stop moving
            rb.velocity = Vector2.zero;
            
        }
        else
        {
            // arrows stop moving
            rb.velocity = Vector2.zero;
            // arrows get planted into whatever they hit, unaffected by gravity, better overall performance
            rb.isKinematic = true;
        }
            
    }
}
//legacy bullet stick under if (colllision...
//gameObject.transform.SetParent(collision.gameObject.transform);