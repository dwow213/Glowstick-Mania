using System.Collections;
using UnityEngine;

public class Idol : MonoBehaviour
{
    [Header("Idol")]
    public Material[] poses;

    [Header("Squash")]
    public float initialSquash; //how squashed the idol should be at the start of her squash
    public float maxSquash; //how squashed the idol should be at the end of her squash (should be normal in terms of height which is 0.6)
    public float squashSteps; //how many times the idol will change size before reaching the end of squash (usually 10)

    Conductor conductor;
    float savedMeasure;
    float savedBeat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        savedMeasure = 1;
        savedBeat = 1;
    }

    // Update is called once per frame
    void Update()
    {
        squash();
        changePose();
    }

    IEnumerator SquashOnBeat()
    {
        print("squashing");
        
        Vector3 tempScale = transform.localScale;
        tempScale.z = initialSquash;
        transform.localScale = tempScale;

        float squashIncrement = (maxSquash - initialSquash) / squashSteps;
        float timeBetweenIncrements = conductor.secPerBeat / squashSteps;

        while (tempScale.z < maxSquash)
        {
            tempScale = transform.localScale;
            tempScale.z += squashIncrement;
            transform.localScale = tempScale;

            yield return new WaitForSeconds(timeBetweenIncrements);
        }
            
        
    }

    void changePose()
    {
        if (savedMeasure >= conductor.currentMeasure)
            return;

        savedMeasure = conductor.currentMeasure;
        gameObject.GetComponent<Renderer>().material = poses[Random.Range(0, poses.Length)];
    }

    void squash()
    {
        if (savedBeat < conductor.totalBeats)
        {
            savedBeat = conductor.totalBeats;
            StartCoroutine(SquashOnBeat());
        }
    }
}
