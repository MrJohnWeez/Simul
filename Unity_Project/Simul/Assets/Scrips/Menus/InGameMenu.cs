using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        CloseAll();
    }

    private void Update() {
        if(InputHandler.instance.GetButtonDown("Menu"))
        {
            if(pauseMenu.activeSelf || settingsMenu.activeSelf)
            {
                CloseAll();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }
    
    public void OpenPauseMenu()
    {
        SettingsController.IsPaused = true;
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        SettingsController.IsPaused = true;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseAll()
    {
        Cursor.visible = false;
        SettingsController.IsPaused = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    
}
