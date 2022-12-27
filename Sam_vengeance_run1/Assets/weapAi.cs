using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapAi : MonoBehaviour
{
    public float joystickFingerId;
    public float rotateFingerId;
    public float horizontalInput;
    public float verticalInput;
    PlayerMove2 playerMove2;

    private void Awake()
    {
        playerMove2 = GameObject.Find("Player").GetComponent<PlayerMove2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object with one finger
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is not being used for the joystick
            if (touch.fingerId != joystickFingerId)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RotateObject(touchPosition);
            }
        }

        // Control the joystick with another finger
        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);

            // Check if the touch is not being used for rotating the object
            if (touch.fingerId != rotateFingerId)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                ControlJoystick(touchPosition);
            }
        }
    }

    void RotateObject(Vector2 touchPosition)
    {
        // Rotate the object based on the touch position
        Touch touch = Input.GetTouch(0);
        float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.rotation = rotation;
        float rotateFingerId = touch.fingerId;
        Debug.Log("Rotating object");
    }

    void ControlJoystick(Vector2 touchPosition)
    {
        // Control the joystick based on the touch position
        // ...
        Touch touch = Input.GetTouch(0);
        if (playerMove2.joystick.Horizontal >= .2f || playerMove2.joystick.Horizontal <= -.2f)
        {
            horizontalInput = playerMove2.joystick.Horizontal;
            joystickFingerId = touch.fingerId;
            Debug.Log("touch updated");
        }
        else horizontalInput = 0;
        joystickFingerId = touch.fingerId;
        Debug.Log("Controlling joystick");
    }

}

