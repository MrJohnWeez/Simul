using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SensitivtySetting
{
    CameraSensitivityY,
    CameraSensitivityX
}
public class SensitivityControl : MonoBehaviour
{
    public Text displayTitle = null;
    public int maxValue = 100;
    public int minValue = 1;
    private int value = 50;
    public SettingsController settingsController = null;
    public SensitivtySetting sensitivtySetting = SensitivtySetting.CameraSensitivityX;
    
    private void OnEnable()
    {
        value = settingsController.GetSensitivySetting(sensitivtySetting);
        displayTitle.text = value.ToString();
    }

    public void SaveSetting()
    {
        settingsController.SaveSensitivySetting(sensitivtySetting, value);
    }

    public void AddTo()
    {
        if(value + 5 <= maxValue)
        {
            value += 5;
            displayTitle.text = value.ToString();
            SaveSetting();
        }
    }

    public void SubtractFrom()
    {
        if(value - 5 >= minValue)
        {
            value -= 5;
            displayTitle.text = value.ToString();
            SaveSetting();
        }
    }
}
