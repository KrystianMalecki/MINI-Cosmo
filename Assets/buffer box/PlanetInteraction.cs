using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlanetInteraction : MonoBehaviour
{
    public GameObject Info;
    public Animator anim;

   
    void OnMouseEnter()
    {
       // Info.SetActive(true);
        anim.Play("inon");
    }
    void OnMouseExit()
    {
      //  Info.SetActive(false);
        
        anim.Play("info");

    }
}
