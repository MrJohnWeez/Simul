using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlsMenu : MonoBehaviour
{
    public GameObject KeyboardMenu = null;
    public GameObject AndroidMenu = null;
    public GameObject XboxMenu = null;
    public GameObject PS4Menu = null;

    private void Start() {
        #if Android
            ShowAndroid();
        #else
        
            ShowKeyboard();
        #endif
    }

    public void ShowKeyboard()
    {
        CloseAll();
        KeyboardMenu.SetActive(true);
    }

    public void ShowAndroid()
    {
        CloseAll();
        AndroidMenu.SetActive(true);
    }

    public void ShowXbox()
    {
        CloseAll();
        XboxMenu.SetActive(true);
    }

    public void ShowPS4()
    {
        CloseAll();
        PS4Menu.SetActive(true);
    }

    public void CloseAll()
    {
        KeyboardMenu.SetActive(false);
        AndroidMenu.SetActive(false);
        XboxMenu.SetActive(false);
        PS4Menu.SetActive(false);
    }
}
