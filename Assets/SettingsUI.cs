using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UIBase
{
    public Toggle RelativeMovementToggle;
    public Toggle FPScounterToggle;
    public override void OpenThis()
    {
        base.OpenThis();
        RelativeMovementToggle.isOn = getSetting("RelativeMovement");
        FPScounterToggle.isOn = getSetting("FPScounter");
    }
    public static bool getSetting(string str)
    {
        if (!PlayerPrefs.HasKey(str))
        {
            PlayerPrefs.SetInt( str, 0);
            Debug.Log("Setting: " + str + " not found. Made new!");
        }
        return (PlayerPrefs.GetInt(str) == 1);
    }
    public static void setSetting(string str, bool b)
    {       
        PlayerPrefs.SetInt(str, b ? 1 : 0);
    }
    public static void toggleSetting(string str)
    {
        setSetting(str, !getSetting(str));
    }
}

