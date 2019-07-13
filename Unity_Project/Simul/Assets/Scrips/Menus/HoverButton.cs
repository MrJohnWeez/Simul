using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> Selects a button if a mouse if hovered on it </summary>
public class HoverButton : MonoBehaviour, IPointerEnterHandler 
{
    private EventSystem m_EventSystem;
    private Button thisButton = null;
    public void OnPointerEnter(PointerEventData eventData)
    {
        #if !UNITY_ANDROID
            MakeSelected();
        #endif
    }
    
    public void MakeSelected()
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
            m_EventSystem.SetSelectedGameObject(gameObject);
        #endif
        
    }
}
