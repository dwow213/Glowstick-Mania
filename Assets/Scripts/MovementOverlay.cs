using UnityEngine;
using UnityEngine.UI;

public class MovementOverlay : MonoBehaviour
{
    public Texture[] forwardMovement;
    public Texture backwardMovement;
    public float overlayAlpha;
    public float overlayAlphaDrain;
    
    EventCore eventCore;
    RawImage image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.processMovement.AddListener(executeMovementOverlay);
        image = GetComponent<RawImage>();

        overlayAlpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Color overlayColor = image.color;
        overlayColor.a = overlayAlpha;
        image.color = overlayColor;

        overlayAlpha = Mathf.Clamp(overlayAlpha - overlayAlphaDrain * Time.deltaTime, 0, 255);

        if (image.texture != forwardMovement[0] && image.texture != forwardMovement[1] && image.texture != forwardMovement[2])
            return;

        if (overlayAlpha < 0.66)
            image.texture = forwardMovement[1];
        else if (overlayAlpha < 0.33)
            image.texture = forwardMovement[2];
    }

    void executeMovementOverlay(bool movingForward)
    {
        overlayAlpha = 1;
        
        if (movingForward)
            image.texture = forwardMovement[0];
        else
            image.texture = backwardMovement;
    }
}
