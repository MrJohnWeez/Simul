using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsController : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool UserInput = true;
    public InputHandler inputHandler;
    public GameObject joystickRightUI;
    public GameObject joystickLeftUI;
    public GameObject highlightJoystickRight;
    public GameObject highlightJoystickLeft;
    public static bool joystickIsRight = true;

    void Start()
    {
        joystickIsRight = PlayerPrefs.GetInt("joystickIsRight")==1?true:false;
        UpdateJoysticks(true);
    }

    public void UpdateJoysticks(bool joystickRight)
    {
        joystickIsRight = joystickRight;
        if(joystickIsRight)
        {
            joystickRightUI.SetActive(true);
            highlightJoystickRight.SetActive(true);
            highlightJoystickLeft.SetActive(false);
            joystickLeftUI.SetActive(false);
        }
        else
        {
            joystickRightUI.SetActive(false);
            joystickLeftUI.SetActive(true);
            highlightJoystickRight.SetActive(false);
            highlightJoystickLeft.SetActive(true);
        }
        PlayerPrefs.SetInt("joystickIsRight", joystickIsRight?1:0);
        inputHandler.UpdateTouchStick();
    }
}
