using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGate : MonoBehaviour
{
    private Animator animator = null;
    private int toogleCount = 0;
    public bool isActiveOnStart = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("DoorDown", false);
    }

    public void SetDoorState(bool DoorIsDown)
    {
        if(DoorIsDown)
        {
            if(!isActiveOnStart)
                toogleCount--;
        
            if(toogleCount == 0)
            {
                animator.SetBool("DoorDown", DoorIsDown);
            }

            if(isActiveOnStart)
                toogleCount--;
        }
        else
        {
            if(isActiveOnStart)
                toogleCount++;

            if(toogleCount == 0)
            {
                animator.SetBool("DoorDown", DoorIsDown);
            }

            if(!isActiveOnStart)
                toogleCount++;
        }
    }
}
