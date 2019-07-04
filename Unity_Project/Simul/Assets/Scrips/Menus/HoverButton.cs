using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler 
{
    private EventSystem m_EventSystem;
    private Button thisButton = null;
    public void OnPointerEnter(PointerEventData eventData) {
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
        m_EventSystem.SetSelectedGameObject(gameObject);
    }
}
