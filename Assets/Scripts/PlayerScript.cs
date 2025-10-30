using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float points = 0;
    public Transform cameraTransform;
    float newCameraTransform = 0f;
    public Slider pointsBar;
    public Image pointsFillBar;
    public RawImage pointPlusOverlay;
    public float pointPlusOverlayAlpha = 0f;
    public RawImage pointMinusOverlay;
    public float pointMinusOverlayAlpha = 0f;
    public float OverlayAlphaDrain = 0.05f;

    void Start()
    {
        newCameraTransform = cameraTransform.position.z;
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


        pointPlusOverlay.color = new Color(255, 255, 255, pointPlusOverlayAlpha);
        pointMinusOverlay.color = new Color(255, 255, 255, pointMinusOverlayAlpha);


        pointPlusOverlayAlpha = Mathf.Clamp(pointPlusOverlayAlpha - OverlayAlphaDrain * Time.deltaTime, 0, 255);
        pointMinusOverlayAlpha = Mathf.Clamp(pointMinusOverlayAlpha - OverlayAlphaDrain * Time.deltaTime, 0, 255);



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
        if (pointChange > 0)
        {
            pointMinusOverlayAlpha = 0;
            pointPlusOverlayAlpha = 1;
        }
        else if (pointChange < 0)
        {
            pointPlusOverlayAlpha = 0;
            pointMinusOverlayAlpha = 1;
        }
            points += pointChange;
        if (points > -5 && points <= 5)
        {
            newCameraTransform = -10f + points;
            
        }
        
        //Debug.Log(newCameraTransform);
    }

    public void DebugButtonPoints(float pointChange)
    {
        PointChange(pointChange);
    }
}
