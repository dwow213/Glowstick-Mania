using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public GameObject joyconObject1;
    public GameObject joyconObject2;

    public GameObject joyconManagerObject;
    public JoyconManager joyconManager;

    public JoyconDemo joycon1;
    public JoyconDemo joycon2;

    public float joycon1Duration = 0;
    public float joycon2Duration = 0;

    public float targetDuration = 2f;

    public int level = 1;

    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joyconManager = joyconManagerObject.GetComponent<JoyconManager>();


        joycon1 = joyconObject1.GetComponent<JoyconDemo>();
        joycon2 = joyconObject2.GetComponent<JoyconDemo>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning(joyconManager.j[0].GetButtonDown(Joycon.Button.SHOULDER_2));
        if (joyconManager.j[0].GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            Debug.LogWarning("works1");
            joycon1Duration = 2f;
        }
   

        if (joyconManager.j[1].GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            Debug.LogWarning("works2");
            joycon2Duration = 2f;
        }
        

        if (joycon1Duration > 0f && joycon2Duration > 0f) 
        {
            playPressed(level);
        }
    }

    public void playPressed(int level)
    {
        SceneManager.LoadScene(level);
    }
}
