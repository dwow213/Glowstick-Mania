using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public GameObject joyconObject1;
    public GameObject joyconObject2;


    public Joycon joycon1;
    public Joycon joycon2;

    public float joycon1Duration = 0;
    public float joycon2Duration = 0;

    public float targetDuration = 2f;

    public int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joycon1 = joyconObject1.GetComponent<Joycon>();
        joycon2 = joyconObject2.GetComponent<Joycon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joycon1.GetButton(Joycon.Button.SL) && joycon1.GetButton(Joycon.Button.SR))
        {
            if (joycon1.GetButtonUp(Joycon.Button.SL) && joycon1.GetButtonUp(Joycon.Button.SR))
            {
                joycon1Duration = 2f;
            }
        }

        if (joycon2.GetButton(Joycon.Button.SL) && joycon2.GetButton(Joycon.Button.SR)) {
            if (joycon2.GetButtonUp(Joycon.Button.SL) && joycon2.GetButtonUp(Joycon.Button.SR))
            {
                joycon2Duration = 2f;
            }
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
