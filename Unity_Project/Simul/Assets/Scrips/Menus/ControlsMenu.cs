using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlsMenu : MonoBehaviour
{
    public GameObject Keyboard = null;
    public GameObject Android = null;
    public GameObject Xbox = null;
    public GameObject PS4 = null;
    public Color DefaultButtonColor;
    public Color SelectedButtonColor;
    public Image KeyboardIg = null;
    public Image AndroidIg = null;
    public Image XboxIg = null;
    public Image PS4Ig = null;

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
        Keyboard.SetActive(true);
        KeyboardIg.color = SelectedButtonColor;
    }

    public void ShowAndroid()
    {
        CloseAll();
        Android.SetActive(true);
        AndroidIg.color = SelectedButtonColor;
    }

    public void ShowXbox()
    {
        CloseAll();
        Xbox.SetActive(true);
        XboxIg.color = SelectedButtonColor;
    }

    public void ShowPS4()
    {
        CloseAll();
        PS4.SetActive(true);
        PS4Ig.color = SelectedButtonColor;
    }

    public void CloseAll()
    {
        Keyboard.SetActive(false);
        Android.SetActive(false);
        Xbox.SetActive(false);
        PS4.SetActive(false);
        KeyboardIg.color = DefaultButtonColor;
        AndroidIg.color = DefaultButtonColor;
        XboxIg.color = DefaultButtonColor;
        PS4Ig.color = DefaultButtonColor;
    }
}
