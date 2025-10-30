using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("Player")]
    public float points = 0;
    public Transform cameraTransform;
    float newCameraTransform = 0f;

    [Header("Points Bar")]
    public Slider pointsBar;
    public Image pointsFillBar;

    [Header("Judgement Overlay")]
    public RawImage pointPlusOverlay;
    public float pointPlusOverlayAlpha = 0f;
    public RawImage pointMinusOverlay;
    public float pointMinusOverlayAlpha = 0f;
    public float OverlayAlphaDrain = 0.05f;

    [Header("Judgement Text")]
    public TextMeshProUGUI judgementText;
    public float judgementTextAlpha = 0f;
    public float judgementAlphaDrain = 0.25f;

    EventCore eventCore;

    void Start()
    {
        newCameraTransform = cameraTransform.position.z;

        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.processJudgement.AddListener(ShowJudgementOverlay);
    }

    void Update()
    {
        points = Mathf.Clamp(points, -6, 5);
        pointsBar.value = points;

        if (points >= 2)
        {
            pointsFillBar.color = Color.greenYellow;

            if (points > 3.5)
            pointsFillBar.color = Color.green;
        }
        else if (points <= -1.5)
        {
            pointsFillBar.color = Color.orange;

            if (points < -3)
            pointsFillBar.color = Color.red;
        }
        else
        {
            pointsFillBar.color = Color.yellow;
        }


        Color pointPlusColor = pointPlusOverlay.color;
        pointPlusColor.a = pointPlusOverlayAlpha;
        pointPlusOverlay.color = pointPlusColor;

        Color judgementTextColor = judgementText.color;
        judgementTextColor.a = judgementTextAlpha;
        judgementText.color = judgementTextColor;

        pointMinusOverlay.color = new Color(255, 255, 255, pointMinusOverlayAlpha);

        pointPlusOverlayAlpha = Mathf.Clamp(pointPlusOverlayAlpha - OverlayAlphaDrain * Time.deltaTime, 0, 255);
        pointMinusOverlayAlpha = Mathf.Clamp(pointMinusOverlayAlpha - OverlayAlphaDrain * Time.deltaTime, 0, 255);
        judgementTextAlpha = Mathf.Clamp(judgementTextAlpha - judgementAlphaDrain * Time.deltaTime, 0, 255);



        // camera movement
        Vector3 cameraOriginal = cameraTransform.position;

        cameraOriginal.z = Mathf.Lerp(cameraOriginal.z, newCameraTransform, 2.0f * Time.deltaTime);

        cameraTransform.position = cameraOriginal;


        // lose condition
        if (points <= -5)
        {
            SceneManager.LoadScene(3);
        }
    }

    // point update for camera movement
    public void PointChange(float pointChange)
    {
        if (pointChange < 0)
        {
            pointPlusOverlayAlpha = 0;

            pointMinusOverlayAlpha = 1;
            judgementTextAlpha = 1;

            judgementText.text = "miss...";
            judgementText.color = Color.red;
        }
            points += pointChange;
        if (points > -5 && points <= 5)
        {
            newCameraTransform = -10f + points;
            
        }
        
        //Debug.Log(newCameraTransform);
    }

    //show the point plus overlay with different colors based on judgement
    void ShowJudgementOverlay(int judgement)
    {
        pointPlusOverlayAlpha = 1;
        judgementTextAlpha = 1;

        pointMinusOverlayAlpha = 0;

        if (judgement == 1)
        {
            pointPlusOverlay.color = Color.yellow;
            judgementText.text = "Okay";
            judgementText.color = Color.yellow;
        }

        else if (judgement == 2)
        {
            pointPlusOverlay.color = Color.green;
            judgementText.text = "Great";
            judgementText.color = Color.lightGreen;
        }

        else if (judgement == 3)
        {
            pointPlusOverlay.color = Color.lightBlue;
            judgementText.text = "PERFECT";
            judgementText.color = Color.lightBlue;
        }
    }

    public void DebugButtonPoints(float pointChange)
    {
        PointChange(pointChange);
    }
}
