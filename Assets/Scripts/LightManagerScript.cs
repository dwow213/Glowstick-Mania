using NUnit.Framework;
using System;
using UnityEngine;

public class LightManagerScript : MonoBehaviour
{

    public GameObject[] redLights;
    float timer = 0;
    float timeLimit = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer > timeLimit)
        {
            lightMove();
            timer = 0;
        }
    }

    void lightMove()
    {
        /*for (int i = 0; i < redLights.Length; i++)
        {
            redLights[i].transform.rotation = new Quaternion(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(0f, 45f), 0, redLights[i].transform.rotation.w);
        }*/

    }


}
