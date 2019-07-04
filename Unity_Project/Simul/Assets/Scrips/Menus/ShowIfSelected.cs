using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowIfSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject setThisActive = null;

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        setThisActive.SetActive(true);
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log(this.gameObject.name + " was DE-selected");
        setThisActive.SetActive(false);
    }
}
