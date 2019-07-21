using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsController : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool UserInput = true;
    public static float CameraSensitivityY = 1;
    public static float CameraSensitivityX = 1;
    public static bool InvertCameraY = false;
    public static bool InvertCameraX = false;
    public static bool InvertMovementY = false;
    public static bool InvertMovementX = false;
    public InputHandler inputHandler;
    public GameObject highlightJoystickRight;
    public GameObject highlightJoystickLeft;
    public static bool joystickIsRight = true;
    private bool oldUserInput = true;
    public GameObject resetMenu = null;
    public SceneController sceneController = null;

    void Start()
    {
        joystickIsRight = PlayerPrefs.GetInt("joystickIsRight")==1?true:false;
        GetInvertAxis(InvertAxis.InvertCameraX);
        GetInvertAxis(InvertAxis.InvertCameraY);
        GetInvertAxis(InvertAxis.InvertMovementX);
        GetInvertAxis(InvertAxis.InvertMovementY);
        GetSensitivySetting(SensitivtySetting.CameraSensitivityX);
        GetSensitivySetting(SensitivtySetting.CameraSensitivityY);
        UpdateJoysticks(joystickIsRight);
        CancelReset();
    }

    /// <summary> Update touch input controls and save the setting </summary>
    public void UpdateJoysticks(bool joystickRight)
    {
        
        joystickIsRight = joystickRight;
        if(joystickIsRight)
        {
            highlightJoystickRight.SetActive(true);
            highlightJoystickLeft.SetActive(false);
        }
        else
        {
            highlightJoystickRight.SetActive(false);
            highlightJoystickLeft.SetActive(true);
        }
        PlayerPrefs.SetInt("joystickIsRight", joystickIsRight?1:0);
        inputHandler.UpdateTouchStick();
    }

    /// <summary> Show the menu that asks the user to confirm it they want to reset the entire game </summary>
    public void ConfirmResetMenu()
    {
        resetMenu.SetActive(true);
    }

    /// <summary> Resets all progress and settings within the game </summary>
    public void ResetEntireGame()
    {
        // Could possibly use the one line of code below to remove all player prefs but it may be bad practice.
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("joystickIsRight", 0);
        PlayerPrefs.SetInt("level1Unlocked", 0);
        PlayerPrefs.SetInt("level2Unlocked", 0);
        PlayerPrefs.SetInt("level3Unlocked", 0);
        PlayerPrefs.SetInt("InvertCameraX", 0);
        PlayerPrefs.SetInt("InvertCameraY", 0);
        PlayerPrefs.SetInt("InvertMovementX", 0);
        PlayerPrefs.SetInt("InvertMovementY", 0);
        SaveSensitivySetting(SensitivtySetting.CameraSensitivityX, 50);
        SaveSensitivySetting(SensitivtySetting.CameraSensitivityY, 50);
        sceneController.MainMenu();
    }

    /// <summary> Hide the reset menu </summary>
    public void CancelReset()
    {
        resetMenu.SetActive(false);
    }

    public void SaveSensitivySetting(SensitivtySetting sensitivtySetting, int newSetting)
    {
        if(sensitivtySetting == SensitivtySetting.CameraSensitivityX)
        {
            PlayerPrefs.SetInt("CameraSensitivityX", newSetting);
            CameraSensitivityX = newSetting;
        }
        else if(sensitivtySetting == SensitivtySetting.CameraSensitivityY)
        {
            CameraSensitivityY = newSetting;
            PlayerPrefs.SetInt("CameraSensitivityY", newSetting);
        }
    }

    public int GetSensitivySetting(SensitivtySetting sensitivtySetting)
    {
        int newValue = 50;
        if(sensitivtySetting == SensitivtySetting.CameraSensitivityX)
        {
            newValue = PlayerPrefs.GetInt("CameraSensitivityX") != 0 ? PlayerPrefs.GetInt("CameraSensitivityX") : 50;
            CameraSensitivityX = newValue;
        }
        else if(sensitivtySetting == SensitivtySetting.CameraSensitivityY)
        {
            newValue = PlayerPrefs.GetInt("CameraSensitivityY") != 0 ? PlayerPrefs.GetInt("CameraSensitivityY") : 50;
            CameraSensitivityY = newValue;
        }
        return newValue;
    }

    public bool GetInvertAxis(InvertAxis axisToUse)
    {
        if(axisToUse == InvertAxis.InvertCameraX)
        {
            InvertCameraX = PlayerPrefs.GetInt("InvertCameraX") == 1;
            return PlayerPrefs.GetInt("InvertCameraX") == 1;
        }
        else if(axisToUse == InvertAxis.InvertCameraY)
        {
            InvertCameraY = PlayerPrefs.GetInt("InvertCameraY") == 1;
            return PlayerPrefs.GetInt("InvertCameraY") == 1;
        }
        else if(axisToUse == InvertAxis.InvertMovementX)
        {
            InvertMovementX = PlayerPrefs.GetInt("InvertMovementX") == 1;
            return PlayerPrefs.GetInt("InvertMovementX") == 1;
        }
        else if(axisToUse == InvertAxis.InvertMovementY)
        {
            InvertMovementY = PlayerPrefs.GetInt("InvertMovementY") == 1;
            return PlayerPrefs.GetInt("InvertMovementY") == 1;
        }
        return false;
    }

    public void SetInvertAxis(InvertAxis axisToUse, bool value)
    {
        if(axisToUse == InvertAxis.InvertCameraX)
        {
            InvertCameraX = value;
            PlayerPrefs.SetInt("InvertCameraX", (value ? 1:0));
        }
        else if(axisToUse == InvertAxis.InvertCameraY)
        {
            InvertCameraY = value;
            PlayerPrefs.SetInt("InvertCameraY", (value ? 1:0));
        }
        else if(axisToUse == InvertAxis.InvertMovementX)
        {
            InvertMovementX = value;
            PlayerPrefs.SetInt("InvertMovementX", (value ? 1:0));
        }
        else if(axisToUse == InvertAxis.InvertMovementY)
        {
            InvertMovementY = value;
            PlayerPrefs.SetInt("InvertMovementY", (value ? 1:0));
        }
        
    }
}
