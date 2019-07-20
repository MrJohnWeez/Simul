using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Handles what menus appear and which ones are toggled off </summary>
public class MainMenu : MonoBehaviour
{
    public static bool forceAboutMenu = false;
    public GameObject mainMenu = null;
    public GameObject aboutMenu = null;
    public GameObject controlsMenu = null;

    private void Start()
    {
        if(forceAboutMenu)
        {
            forceAboutMenu = false;
            OpenAboutMenu();
        }
        else
        {
            OpenMainMenu();
        }
    }

    public void OpenAboutMenu()
    {
        CloseAll();
        aboutMenu.SetActive(true);
    }

    public void OpenControlsMenu()
    {
        CloseAll();
        controlsMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        CloseAll();
        mainMenu.SetActive(true);
    }

    public void CloseAll()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
}
