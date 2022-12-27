using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchTesting : MonoBehaviour
{
    PlayerMove2 playerMove2;
    public float randoTouch;
    public int joyTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        playerMove2 = GameObject.Find("Player").GetComponent<PlayerMove2>();
    }

    // Update is called once per frame
    void Update()
    {

        
                
                randoTouch = -1;

        if (Input.touchCount > 0)
        {  
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (joyTouch ==-1)
                {
                    randoTouch = Input.GetTouch(i).fingerId;
                    //print("joy= " + joyTouch + "& rand is" + randoTouch+ " in if statement");
                }
                if (joyTouch ==0)
                {
                    randoTouch = -1;
                    //randoTouch= Input.GetTouch(i).fingerId;
                    //Debug.Log("joy on as " + joyTouch + " and rando = " + randoTouch + "in last if");                   
                
                }                
            }
        }
    }
}
