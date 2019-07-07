using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
                Debug.Log("Selected Again");
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
