using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
#pragma warning disable 0618 // disable network obsolete warning

/// <summary> Handles what menus appear and which ones are toggled off </summary>
public class MainMenu : MonoBehaviour
{
    public GameObject aboutMenu = null;
    public GameObject controlsMenu = null;

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

    public void OpenAboutMenu()
    {
        CloseAll();
        ToggleAboutMenu(true);
    }

    /// <summary> Close all menus on main menu screen </summary>
    public void CloseAll()
    {
        aboutMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    private void ToggleAboutMenu(bool activeState)
    {
        aboutMenu.SetActive(activeState);
    }
}
