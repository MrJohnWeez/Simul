using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Will notify PressurePlate objects when a block that has the tag "IsWeight" is inside the trigger </summary>
public class PressureToggle : MonoBehaviour
{
    public Material triggeredMat = null;
    public Material nonTriggeredMat = null;
    public PressureGate[] TriggedObjects = null;
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
            }
            else
            {
                render.material = nonTriggeredMat;
            }
            
        }
    }

    public bool IsTriggered()
    {
        return isTriggered;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("IsWeight"))
        {
            isTriggered = true;
            foreach (PressureGate pg in TriggedObjects)
            {
                pg.DisableGate();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("IsWeight"))
        {
            isTriggered = false;
            foreach (PressureGate pg in TriggedObjects)
            {
                pg.EnableGate();
            }
        }
    }
}
