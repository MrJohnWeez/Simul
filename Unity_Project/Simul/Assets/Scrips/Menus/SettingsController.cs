using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsController : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool UserInput = true;
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
        sceneController.MainMenu();
    }

    /// <summary> Hide the reset menu </summary>
    public void CancelReset()
    {
        resetMenu.SetActive(false);
    }

    private void Update() {

        // Update the correct touch joystick when input has been disabled
        // if(oldUserInput != UserInput)
        // {
        //     if(!UserInput)
        //     {
        //         joystickRightUI.SetActive(false);
        //         joystickLeftUI.SetActive(false);
        //         inputHandler.UpdateTouchStick();
        //     }
        //     else
        //     {
        //         if(joystickIsRight)
        //         {
        //             joystickRightUI.SetActive(true);
        //             joystickLeftUI.SetActive(false);
        //         }
        //         else
        //         {
        //             joystickRightUI.SetActive(false);
        //             joystickLeftUI.SetActive(true);
        //         }
        //     }
        // }
        
        // oldUserInput = UserInput;
    }
}
