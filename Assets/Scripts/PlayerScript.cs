using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float points = 0;
    public Transform cameraTransform;
    float newCameraTransform = 0f;
    public Slider pointsBar;

    void Start()
    {
        newCameraTransform = cameraTransform.position.z;
    }

    void Update()
    {
        
        points = Mathf.Clamp(points, -6, 5);
        pointsBar.value = points;


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
