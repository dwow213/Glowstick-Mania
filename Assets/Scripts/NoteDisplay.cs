using UnityEngine;
using UnityEngine.UI;

public class NoteDisplay : MonoBehaviour
{
    [Header("3D")]
    public Material[] poses;

    [Header("2D (canvas)")]
    public Texture up;
    public Texture down;
    public Texture left;
    public Texture right;
    public RawImage image;

    Renderer selfRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setNote(float note)
    {
        //3d
        if (selfRenderer != null)
        {
            selfRenderer.material = poses[(int)note - 1];
            return;
        }
        
        //2d
        if (note == 1)
        {
            image.texture = up;
        }
            
        else if (note == 2)
        {
            image.texture = down;
        }
            
        else if (note == 3)
        {
            image.texture = left;
        }
            
        else if (note == 4)
        {
            image.texture = right;
        }
            
    }
}
