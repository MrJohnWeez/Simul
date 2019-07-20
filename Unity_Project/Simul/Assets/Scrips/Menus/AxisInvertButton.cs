using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InvertAxis
{
    InvertCameraY,
    InvertCameraX,
    InvertMovementY,
    InvertMovementX
}
public class AxisInvertButton : MonoBehaviour
{
    private bool isInverted = false;
    public Text Title = null;
    public InvertAxis invertAxis = InvertAxis.InvertCameraX;
    public SettingsController settingsController = null;

    private void OnEnable()
    {
        isInverted = settingsController.GetInvertAxis(invertAxis);
        UpdateTitle();
    }

    public void ToggleState()
    {
        isInverted = !isInverted;
        UpdateTitle();
        settingsController.SetInvertAxis(invertAxis, isInverted);
    }

    public void UpdateTitle()
    {
        if(isInverted)
        {
            Title.text = "YES";
        }
        else
        {
            Title.text = "NO";
        }
    }
}
