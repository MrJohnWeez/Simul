using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlButtonToggle : MonoBehaviour, ISelectHandler
{
    public ControlsMenu controlsMenu = null;
    public int menuValue = 0;

    public void OnSelect(BaseEventData eventData)
    {
        controlsMenu.ShowControls(menuValue);
    }

    // IDeselectHandler
    // public void OnDeselect(BaseEventData data)
    // {
    //     Debug.Log(this.gameObject.name + " was DE-selected");
    // }
}
