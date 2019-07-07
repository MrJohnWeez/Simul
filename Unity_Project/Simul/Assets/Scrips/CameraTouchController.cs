using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CameraTouchController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static Vector2 delta = new Vector2();

    public void OnDrag(PointerEventData data)
    {
        if (data.dragging && SettingsController.UserInput)
        {
            delta = data.delta;
            delta.x = delta.x / 25;
            delta.y = delta.y / 25;
        }
        else
        {
            delta = new Vector2();
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        delta = new Vector2();
    }
}
