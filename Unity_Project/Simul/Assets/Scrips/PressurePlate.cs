using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Will notify PressurePlate objects when a block that has the tag "IsWeight" is inside the trigger </summary>
public class PressurePlate : MonoBehaviour
{
    public Material triggeredMat = null;
    public Material nonTriggeredMat = null;
    private bool isTriggered = false;
    private bool preIsTriggered = false;
    private MeshRenderer render = null;

    private LevelControllerBase levelController;
    
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        levelController = GameObject.FindWithTag("GameController").GetComponent<LevelControllerBase>();
    }

    void Update()
    {
        // If trigger status has changed update the color of plate and
        // notify any waiting objects
        if(isTriggered != preIsTriggered)
        {
            levelController.UpdateTriggers();
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
