using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlanetInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Info;
    public Animator anim;
    public void Start()
    {
        Info = Instantiate(StaticDataManager.instance.CSOInfo,transform);
        anim = Info.GetComponent<Animator>();
        anim.Play("info",0,1);

    }
    public void OnPointerEnter(PointerEventData eventData)
      {
          anim.Play("inon");
      }
      public void OnPointerExit(PointerEventData eventData)
      {

          anim.Play("info");
      }
   /* void OnMouseDown()
    {
        // Info.SetActive(true);
        // anim.Play("inon");
        Debug.Log("on yo mum");
    }
    void OnMouseEnter()
     {
        // Info.SetActive(true);
        // anim.Play("inon");
        Debug.Log("yo");
     }
     void OnMouseExit()
     {
        //  Info.SetActive(false);

        // anim.Play("info");
        Debug.Log("bye");

     }*/
}
