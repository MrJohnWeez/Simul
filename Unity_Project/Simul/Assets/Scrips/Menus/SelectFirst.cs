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
        MakeSelected();
    }
    private void Start() {
        MakeSelected();
    }

    public void MakeSelected()
    {
        if(!thisButton)
        {
            thisButton = GetComponent<Button>();
        }
        if(!m_EventSystem)
        {
            m_EventSystem = EventSystem.current;
        }
        thisButton.Select();
        thisButton.OnSelect(null);
    }

    private void Update() {
        //Debug.Log("Selected: " + m_EventSystem.currentSelectedGameObject);
    }
}
