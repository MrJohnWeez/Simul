using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject ControlsMenu;

    void Start()
    {
        CloseAll(false);
    }

    private void Update() {
        if(InputHandler.instance.GetButtonDown("Menu"))
        {
            if(pauseMenu.activeSelf || settingsMenu.activeSelf)
            {
                CloseAll();
            }
            else if(SettingsController.UserInput)
            {
                OpenPauseMenu();
            }
        }
    }
    
    public void OpenControlsMenu()
    {
        SettingsController.UserInput = false;
        SettingsController.IsPaused = true;
        ControlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

    }
    public void OpenPauseMenu()
    {
        SettingsController.UserInput = false;
        SettingsController.IsPaused = true;
        pauseMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        SettingsController.UserInput = false;
        SettingsController.IsPaused = true;
        pauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseAll(bool overrideSettings = true)
    {
        if(overrideSettings)
        {
            SettingsController.UserInput = true;
        }
        SettingsController.IsPaused = false;
        ControlsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    
}
