 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class WeapCont : MonoBehaviour
{
    touchTesting touchScript;
    PlayerMove2 playerMove2;
    public bool rotatingg;
    public GameObject arrow;
    public float launchForce;
    public Transform shotpoint;
    public float fireRate = .5f;
    private float nextFire = 0f;
    private bool if1;
    private bool if2;
    private bool if3;
    private bool if4;
    private bool if5;
    private bool if6;
    [SerializeField] private GameObject[] Rounds;


    private SpriteRenderer spriteRend;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        touchScript = GameObject.Find("weapon").GetComponent<touchTesting>();
       
        

    }
    

    //awake is called before start and is active even if script component is turned off 
    private void Awake()
    {
        playerMove2 = GameObject.Find("Player").GetComponent<PlayerMove2>();
    }

    private void rotateWeapon (Vector2 touchPosition, int orientation)
    {
        
        float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - orientation, Vector3.forward);
        transform.rotation = rotation;
        //Debug.Log("rotating weapon");
        

        
    }


    // Update is called once per frame
    void Update()
    {
        //touch.fingerId = unique index for touch
        //touch.position = position of touch screen space pixels 
        //input.GetTouch(index) = retrieves info about the touch detected
        //input.touchCount = current number of screen touches



        //shoot method call conditions
        if (if1 == true || if2 == true || if3 == true || if4 == true || if5 == true || if6 == true)
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                shoot();

            }

        

        


         //no joystick, just shooting
        if (Input.touchCount > 0 && playerMove2.transform.localScale.x == 1 && touchScript.joyTouch == -1 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position; 
            rotateWeapon(touchPosition, 0);
            //print("if 1");
            rotatingg = true;
            if1=true;
        }
        else 
            if1=false;
        // facing left, no joystick, just shooting
        if (Input.touchCount > 0 && playerMove2.transform.localScale.x == -1 && touchScript.joyTouch == -1 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
            rotateWeapon(touchPosition, -180);
            //print("if 2");
            rotatingg = true;
            if2 = true;
        }
        else
            if2 = false;

        //no shooting, just joystick, either direction
        if (Input.touchCount== 1 && touchScript.joyTouch==0) 
        { 
            rotatingg = false;
            if1 = false;
            if2= false;
            if3= false;
            if4= false;
            if5= false;
            if6= false;
        }

        //joystick on, spin with other finger (gettouch1 works)
        //spinning with other finger, now joystick on (gettouch0 works)
        

        {
            {   //touching joystick, then start shooting
                if (Input.touchCount > 1 && playerMove2.transform.localScale.x == 1 && Mathf.Abs(playerMove2.joystick.Horizontal) + Mathf.Abs(playerMove2.joystick.Vertical) > 0)
                {
                    Touch touch = Input.GetTouch(1);
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    rotateWeapon(touchPosition, 0);
                    //print("if 5");
                    if5= true;
                }
                else
                    if5 = false;
                
                //shooting with one or more finger, then touch joystick 
                if (Input.touchCount > 1 && playerMove2.transform.localScale.x == 1 && rotatingg==true)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    rotateWeapon(touchPosition, 0);
                    //print("if 3");
                    if3= true;
                }
                else
                    if3= false;

                //facing left. joystick then shooting
                if (Input.touchCount > 1 && playerMove2.transform.localScale.x == -1 && Mathf.Abs(playerMove2.joystick.Horizontal) + Mathf.Abs(playerMove2.joystick.Vertical) > 0)
                {
                    Touch touch = Input.GetTouch(1);
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    rotateWeapon(touchPosition, -180);
                    //print("if 6");
                    if6= true;
                }
                else
                    if6= false;

                //facing left, shooting, then joystick
                if (Input.touchCount > 1 && playerMove2.transform.localScale.x == -1 && rotatingg== true)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    rotateWeapon(touchPosition, -180);
                    //print("if 4");
                    if4= true;
                }
                else
                    if4 = false;
            }
                
            {
                

                
            }
        }




        //flipped correctly when behind back
        if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90)
            spriteRend.flipY = true;
        else
        {
            spriteRend.flipY = false;
        }
        //print("if1 =" + if1 + "   if2 is " + if2 + "   if3 is " + if3 + "   if4 is " + if4 + "   if5 is " + if5 + "   if6 is " + if6);

    }
    void shoot()
    {

        Rounds[FindRound()].transform.position = shotpoint.position;
        Rounds[FindRound()].transform.rotation = shotpoint.rotation;
        Rounds[FindRound()].GetComponent<Bullets>().NewShot();
        
        

        //GameObject newBullet =  
        //bulletss.hit = false;
        //bulletss.boxCollider.enabled = true;

        //Legacy for arrows
        //            if (playerMove2.transform.localScale.x == 1)
        {
            //GameObject newArrow = Instantiate(arrow, shotpoint.position, shotpoint.rotation);
            //newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        }

        //if (playerMove2.transform.localScale.x == -1)
        {
            //GameObject newArrow = Instantiate(arrow, shotpoint.position, shotpoint.rotation);
            //newArrow.GetComponent<Rigidbody2D>().velocity = -transform.right * launchForce;
        }
    }
    private int FindRound()
    {
        for (int i=0; i< Rounds.Length; i++ )
        {
            if (!Rounds[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
