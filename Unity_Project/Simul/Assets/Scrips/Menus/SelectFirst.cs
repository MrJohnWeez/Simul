using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> Selects a button first so that controller navigation can take place.true
/// Also if the user unselects all buttons this will be the first button to be selected again </summary>
public class SelectFirst : MonoBehaviour
{
    private EventSystem m_EventSystem;
    private Button thisButton = null;

    private void OnEnable()
    {
        #if !UNITY_ANDROID
            MakeSelected(true);
        #endif
    }

    /// <summary> Select this button and notify the event system of the change </summary>
    public void MakeSelected(bool forceSelect = false)
    {
        #if !UNITY_ANDROID
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
                //Debug.Log("Selected Again");
                thisButton.Select();
                thisButton.OnSelect(null);
            }
        #endif
        
    }

    private void Update()
    {
        #if !UNITY_ANDROID
            MakeSelected();
        #endif
    }
}
