using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GameObject joystickRightUI;
    public GameObject joystickLeftUI;
    public InputHandler inputHandler;
    private bool preSetting = false;
    private bool oldUserInput = false;
    
    private void Start()
    {
        UpdateJoysticks(SettingsController.joystickIsRight);
        preSetting = SettingsController.joystickIsRight;
    }
    void Update()
    {
        if(preSetting != SettingsController.joystickIsRight)
        {
            preSetting = SettingsController.joystickIsRight;
            UpdateJoysticks(SettingsController.joystickIsRight);
        }

        if(oldUserInput != SettingsController.UserInput)
        {
            oldUserInput = SettingsController.UserInput;

            if(!SettingsController.UserInput)
            {
                joystickRightUI.SetActive(false);
                joystickLeftUI.SetActive(false);
                inputHandler.UpdateTouchStick();
            }
            else
            {
                if(SettingsController.joystickIsRight)
                {
                    joystickRightUI.SetActive(true);
                    joystickLeftUI.SetActive(false);
                }
                else
                {
                    joystickRightUI.SetActive(false);
                    joystickLeftUI.SetActive(true);
                }
            }
        }
    }

    public void UpdateJoysticks(bool joystickRight)
    {
        if(joystickRight)
        {
            joystickRightUI.SetActive(true);
            joystickLeftUI.SetActive(false);
        }
        else
        {
            joystickRightUI.SetActive(false);
            joystickLeftUI.SetActive(true);
        }
        inputHandler.UpdateTouchStick();
    }
}
