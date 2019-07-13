using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Disables Gameobject if touch input is not supported </summary>
public class TouchDependent : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(Input.touchSupported);
    }
}
