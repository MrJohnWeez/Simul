using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
#pragma warning disable 0618 // disable network obsolete warning

/// <summary> Handles what menus appear and which ones are toggled off </summary>
public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayMenu = null;
    public GameObject aboutMenu = null;

    private void Start()
    {
        CloseAll();
        GameObject networked = GameObject.Find("MyNetworkManager");
        if(networked)
        {
            NetworkManager.Shutdown();
            Destroy(networked);
        }
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
