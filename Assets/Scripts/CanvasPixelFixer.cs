using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPixelFixer : MonoBehaviour
{
    public Vector3 v3= new Vector3(0,0.08f,0);
    void Start()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
        float nextX = Mathf.Round(32 * newPosition.x);
        float nextY = Mathf.Round(32 * newPosition.y);
        transform.position = new Vector3(nextX / 32, nextY / 32, 0) +v3;
    }

    
}
