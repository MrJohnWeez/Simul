using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlButtonToggle : MonoBehaviour, ISelectHandler
{
    public ControlsMenu controlsMenu = null;
    public int menuValue = 0;
    public bool selectFirstWindows = false;
    public bool selectFirstAndroid = false;
    private EventSystem m_EventSystem;
    private Button thisButton = null;


    public void OnSelect(BaseEventData eventData)
    {
        controlsMenu.ShowControls(menuValue);
    }
    
    private void OnEnable()
    {
        #if UNITY_ANDROID
            if(selectFirstAndroid)
                MakeSelected(true);
        #else
            if(selectFirstWindows)
                MakeSelected(true);
        #endif
    }

    public void MakeSelected(bool forceSelect = false)
    {
        #if UNITY_ANDROID
            if(selectFirstAndroid)
            {
                if(!thisButton)
                {
                    thisButton = GetComponent<Button>();
                }
                if(!m_EventSystem)
                {
                    m_EventSystem = EventSystem.current;
                }
                if(m_EventSystem.currentSelectedGameObject == null || forceSelect)
                {
                    Debug.Log("Selected Again");
                    thisButton.Select();
                    thisButton.OnSelect(null);
                }
            }
        #else
            if(selectFirstWindows)
            {
                if(!thisButton)
                {
                    thisButton = GetComponent<Button>();
                }
                if(!m_EventSystem)
                {
                    m_EventSystem = EventSystem.current;
                }
                if(m_EventSystem.currentSelectedGameObject == null || forceSelect)
                {
                    Debug.Log("Selected Again");
                    thisButton.Select();
                    thisButton.OnSelect(null);
                }
            }
        #endif
    }

    private void Update()
    {
        #if UNITY_ANDROID
            if(selectFirstAndroid)
                MakeSelected();
        #else
            if(selectFirstWindows)
                MakeSelected();
        #endif
    }
}
