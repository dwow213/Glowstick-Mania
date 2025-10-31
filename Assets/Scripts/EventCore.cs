using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    //event for player input 
    public UnityEvent<string> provideInput;
    //event for when player does the wrong movement
    public UnityEvent wrongMovement;

    //event for sending out the judgement, for the visual stuff
    public UnityEvent<int> processJudgement;

    public UnityEvent<bool> processMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
