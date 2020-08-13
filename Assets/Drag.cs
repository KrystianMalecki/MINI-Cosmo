using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour, IDragHandler
{
    public float f = 10;
    public void OnDrag(PointerEventData eventData)
    {
       
       
      //  transform.position = Camera.main.ScreenToViewportPoint( eventData.position *f+new Vector2(Camera.main.pixelWidth/2,Camera.main.pixelHeight/2)) ;
    }
}
