using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public static bool SountIsOn = true;
    public Sprite soundOn = null;
    public Sprite soundOff = null;
    public Image soundImage = null;
    void Start()
    {
        UpdateImage();
    }


    public void ToggleSound()
    {
        SountIsOn = !SountIsOn;
        UpdateImage();
    }

    public void UpdateImage()
    {
        if(SoundControl.SountIsOn)
        {
            soundImage.sprite = soundOn;
        }
        else
        {
            soundImage.sprite = soundOff;
        }
    }
}
