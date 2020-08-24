using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPScounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Start()
    {
        StartCoroutine("step");
    }
    IEnumerator step()
    {
        while (true)
        {

            text.text = (1f / Time.unscaledDeltaTime).ToString("0")+" FPS";

            yield return new WaitForSeconds(Time.timeScale);

        }
    }
    public void toggle()
    {
     gameObject.SetActive(  SettingsUI.getSetting("FPScounter"));
        
      
    }
}
