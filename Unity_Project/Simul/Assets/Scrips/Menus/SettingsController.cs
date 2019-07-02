using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsController : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool UserInput = true;
    public InputHandler inputHandler;
    public Color selectedButtonColor;
    public Color unselectedColor;
    public Image movementJoystickLeftButton;
    public Image movementJoystickRightButton;
    public GameObject joystickRightUI;
    public GameObject joystickLeftUI;
    public static bool joystickIsRight = true;

    void Start()
    {
        UpdateJoysticks(true);
    }

    public void UpdateJoysticks(bool joystickRight)
    {
        joystickIsRight = joystickRight;
        if(joystickIsRight)
        {
            movementJoystickRightButton.color = selectedButtonColor;
            movementJoystickLeftButton.color = unselectedColor;
            joystickRightUI.SetActive(true);
            joystickLeftUI.SetActive(false);
        }
        else
        {
            movementJoystickRightButton.color = unselectedColor;
            movementJoystickLeftButton.color = selectedButtonColor;
            joystickRightUI.SetActive(false);
            joystickLeftUI.SetActive(true);
        }
        inputHandler.UpdateTouchStick();
    }
}
