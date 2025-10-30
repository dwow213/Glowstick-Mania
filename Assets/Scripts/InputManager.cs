using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Joycons")]
    public GameObject joycon1;
    public GameObject joycon2;

    [Header("Detection Zones")]
    public LayerMask MotionDetectionZone;
    public GameObject topZone;
    public GameObject leftZone;
    public GameObject bottomZone;
    public GameObject rightZone;

    bool doingMovement;

    EventCore eventCore;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.wrongMovement.AddListener(movementIsWrong);
        doingMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    //getting input from player
    //currently uses arrow keys for testing, but will be set up for motion detection in the future
    void GetInput()
    {
        string collision1 = null;
        string collision2 = null;

        RaycastHit joycon1Hit;
        Physics.Raycast(joycon1.transform.position, joycon1.transform.forward, out joycon1Hit, Mathf.Infinity, MotionDetectionZone);
        RaycastHit joycon2Hit;
        Physics.Raycast(joycon2.transform.position, joycon2.transform.forward, out joycon2Hit, Mathf.Infinity, MotionDetectionZone);

        if (joycon1Hit.collider != null)
            collision1 = joycon1Hit.collider.gameObject.name;

        if (joycon2Hit.collider != null)
            collision2 = joycon2Hit.collider.gameObject.name;
        
        if (collision1 == "topZone" && collision2 == "topZone")
        {
            if (!doingMovement)
            {
                eventCore.provideInput.Invoke("up");
                print("up");
            }
            doingMovement = true;
        }
        else if (collision1 == "bottomZone" && collision2 == "bottomZone")
        {
            if (!doingMovement)
            {
                eventCore.provideInput.Invoke("down");
                print("down");
            }
            doingMovement = true;
            
        }
        else if (collision1 == "leftZone" && collision2 == "leftZone")
        {
            if (!doingMovement)
            {
                eventCore.provideInput.Invoke("left");
                print("left");
            } 
            doingMovement = true;
        }
        else if (collision1 == "rightZone" && collision2 == "rightZone")
        {
            if (!doingMovement)
            {
                eventCore.provideInput.Invoke("right");
                print("right");
            }
            doingMovement = true;
        }
        else if (collision1 == null && collision2 == null)
        {
            doingMovement = false;
            //print("setting doingMovement to false");
        }

        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            eventCore.provideInput.Invoke("up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            eventCore.provideInput.Invoke("down");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            eventCore.provideInput.Invoke("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            eventCore.provideInput.Invoke("right");
        } */
    }

    //just allows the player to do another movement if they did the wrong one
    void movementIsWrong()
    {
        //print("receiving event for movement is wrong");
        doingMovement = false;
    }
}
