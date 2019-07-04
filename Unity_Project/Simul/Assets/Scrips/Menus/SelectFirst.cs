using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectFirst : MonoBehaviour
{
    private EventSystem m_EventSystem;
    private Button thisButton = null;

    private void OnEnable() {
        MakeSelected(true);
    }

    public void MakeSelected(bool forceSelect = false)
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

    private void Update() {
        MakeSelected();
        //Debug.Log("Selected: " + m_EventSystem.currentSelectedGameObject);
    }
}
