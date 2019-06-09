using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Will notify PressurePlate objects when a block that has the tag "IsWeight" is inside the trigger </summary>
public class PressurePlate : MonoBehaviour
{
    public PressureGate[] ToggleObjects;
    public Material triggeredMat = null;
    public Material nonTriggeredMat = null;
    private bool isTriggered = false;
    private bool preIsTriggered = false;
    private MeshRenderer render = null;
    
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // If trigger status has changed update the color of plate and
        // notify any waiting objects
        if(isTriggered != preIsTriggered)
        {
            preIsTriggered = isTriggered;
            if(isTriggered)
            {
                render.material = triggeredMat;
                foreach (PressureGate pg in ToggleObjects)
                {
                    pg.DisableGate();
                }
            }
            else
            {
                render.material = nonTriggeredMat;
                foreach (PressureGate pg in ToggleObjects)
                {
                    pg.EnableGate();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("IsWeight"))
        {
            isTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("IsWeight"))
        {
            //print("Triggered!");
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("IsWeight"))
        {
            isTriggered = false;
        }
    }
}
