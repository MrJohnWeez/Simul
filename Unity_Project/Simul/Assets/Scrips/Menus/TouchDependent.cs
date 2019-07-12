using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disables Gameobject if touch input is not supported
public class TouchDependent : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(Input.touchSupported);
    }
}
