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
    private bool oldUserInput = true;
    public GameObject resetMenu = null;
    public SceneController sceneController = null;

    void Start()
    {
        joystickIsRight = PlayerPrefs.GetInt("joystickIsRight")==1?true:false;
        UpdateJoysticks(joystickIsRight);
        CancelReset();
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

    public void ConfirmResetMenu()
    {
        resetMenu.SetActive(true);
    }

    public void ResetEntireGame()
    {
        // Could possibly use the one line of code below to remove all player prefs but it may be bad practice.
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("joystickIsRight", 0);
        PlayerPrefs.SetInt("tutorialLevelUnlocked", 1);
        PlayerPrefs.SetInt("level1Unlocked", 0);
        PlayerPrefs.SetInt("level2Unlocked", 0);
        PlayerPrefs.SetInt("level3Unlocked", 0);
        sceneController.MainMenu();
    }

    public void CancelReset()
    {
        resetMenu.SetActive(false);
    }

    private void Update() {
        if(oldUserInput != UserInput)
        {
            if(!UserInput)
            {
                joystickRightUI.SetActive(false);
                joystickLeftUI.SetActive(false);
                inputHandler.UpdateTouchStick();
            }
            else
            {
                if(joystickIsRight)
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
        
        oldUserInput = UserInput;
    }
}
