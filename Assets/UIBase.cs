using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public bool IsOpen;
   public virtual void OpenThis()
    {
        gameObject.SetActive(true);
        IsOpen = true;
    }
    public virtual void CloseThis()
    {
        gameObject.SetActive(false);
        IsOpen = false;

    }
}
