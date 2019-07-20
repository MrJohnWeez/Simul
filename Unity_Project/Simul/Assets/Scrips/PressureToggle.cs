using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Will notify PressurePlate objects when a block that has the tag "IsWeight" is inside the trigger </summary>
public class PressureToggle : MonoBehaviour
{
    public bool playerCanTrigger = false;
    public PressureGate[] TriggedObjects = null;
    public PressureGate[] TriggedObjectsOn = null;
    public CableTrigger[] ConnectedCables = null;
    public AudioSource AudioClip;
    private bool isTriggered = false;
    private bool preIsTriggered = false;
    private MeshRenderer render = null;
    private Animator animator = null;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isTriggered != preIsTriggered)
        {
            preIsTriggered = isTriggered;
            animator.SetBool("IsPressed", isTriggered);
        }
    }

    public bool IsTriggered()
    {
        return isTriggered;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("IsWeight") || (playerCanTrigger && other.CompareTag("Player")))
        {
            isTriggered = true;
            foreach (PressureGate pg in TriggedObjects)
            {
                pg.DisableGate();
            }
            foreach (PressureGate pg in TriggedObjectsOn)
            {
                pg.EnableGate();
            }
            foreach (CableTrigger ct in ConnectedCables)
            {
                ct.EnableCable();
            }
            AudioClip.Play();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("IsWeight") || (playerCanTrigger && other.CompareTag("Player")))
        {
            isTriggered = false;
            foreach (PressureGate pg in TriggedObjects)
            {
                pg.EnableGate();
            }
            foreach (PressureGate pg in TriggedObjectsOn)
            {
                pg.DisableGate();
            }
            foreach (CableTrigger ct in ConnectedCables)
            {
                ct.DisableCable();
            }
        }
    }
}
