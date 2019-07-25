using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    private void OnDisable() {
        background.gameObject.SetActive(false);
        base.MakeZero();
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerId < 0 || !SettingsController.UserInput || !SettingsController.IsPaused)
        {
            return;
        }
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerId < 0)
        {
            return;
        }
            
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}