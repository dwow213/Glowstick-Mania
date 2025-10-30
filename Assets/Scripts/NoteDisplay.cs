using UnityEngine;
using UnityEngine.UI;

public class NoteDisplay : MonoBehaviour
{
    //temporary, only for showcasing the note is changing
    
    public Texture up;
    public Texture down;
    public Texture left;
    public Texture right;

    public RawImage image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNote(float note)
    {
        if (note == 1)
        {
            //if ()
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
