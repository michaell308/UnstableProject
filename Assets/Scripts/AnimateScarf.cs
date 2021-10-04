using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScarf : MonoBehaviour
{
    public Transform scarf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveScarf(int moveDown)
    {
        Debug.Log("move scarf: " + moveDown);
        float newYPosition = scarf.position.y;
        Debug.Log("original y position: " + newYPosition);
        if (moveDown == 0)
        {
            newYPosition -= 0.05f;    
        }
        else
        {
            newYPosition += 0.05f;
        }
        Debug.Log("newYPosition: " + newYPosition);
        scarf.position = new Vector3(scarf.position.x, newYPosition, scarf.position.z);
    }
}
