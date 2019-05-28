using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Handles what menus appear and which ones are toggled off </summary>
public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayMenu = null;
    public GameObject aboutMenu = null;

    private void Start()
    {
        CloseAll();
    }

    public void OpenHowToPlay()
    {
        CloseAll();
        ToggleHowToPlayMenu(true);
    }

    public void OpenAboutMenu()
    {
        CloseAll();
        ToggleAboutMenu(true);
    }

    /// <summary> Close all menus on main menu screen </summary>
    public void CloseAll()
    {
        ToggleHowToPlayMenu(false);
        ToggleAboutMenu(false);
    }

    private void ToggleHowToPlayMenu(bool activeState)
    {
        howToPlayMenu.SetActive(activeState);
    }

    private void ToggleAboutMenu(bool activeState)
    {
        aboutMenu.SetActive(activeState);
    }
}
