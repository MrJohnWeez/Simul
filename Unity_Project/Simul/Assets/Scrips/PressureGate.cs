using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows functions to get ran when a pressure plate is trigged </summary>
public class PressureGate : MonoBehaviour
{
    private int toogleCount = 0;
    public bool outPutDebugInfo = false;
    public bool isActiveOnStart = true;

    private void Start() {
        //gameObject.SetActive(isActiveOnStart);
    }
    private void Update() {
        if(outPutDebugInfo)
            Debug.Log("toogleCount: " + toogleCount);
    }


    /// <summary> Gate should no longer be blocking path </summary>
    public void DisableGate()
    {
        if(!isActiveOnStart)
            toogleCount--;
        
        if(toogleCount == 0)
        {
            gameObject.SetActive(false);
        }

        if(isActiveOnStart)
            toogleCount--;
    }

    /// <summary> Gate should block path again </summary>
    public void EnableGate()
    {
        if(isActiveOnStart)
            toogleCount++;

        if(toogleCount == 0)
        {
            gameObject.SetActive(true);
        }

        if(!isActiveOnStart)
            toogleCount++;
    }
}
