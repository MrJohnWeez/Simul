using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows functions to get ran when a pressure plate is trigged </summary>
public class PressureGate : MonoBehaviour
{
    private bool startingActiveState;

    /// <summary> Gate should no longer be blocking path </summary>
    public void DisableGate()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    /// <summary> Gate should block path again </summary>
    public void EnableGate()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
