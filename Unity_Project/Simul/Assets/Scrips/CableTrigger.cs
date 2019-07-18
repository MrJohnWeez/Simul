using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableTrigger : MonoBehaviour
{
    private int toogleCount = 0;
    public bool outPutDebugInfo = false;
    public bool isActiveOnStart = false;
    public Animator animator = null;

    private void Start() {
        animator.SetBool("IsActivated", isActiveOnStart);
    }
    private void Update() {
        if(outPutDebugInfo)
            Debug.Log("toogleCount: " + toogleCount);
    }


    /// <summary> Gate should no longer be blocking path </summary>
    public void DisableCable()
    {
        if(!isActiveOnStart)
            toogleCount--;
        
        if(toogleCount == 0)
        {
            animator.SetBool("IsActivated", false);
        }

        if(isActiveOnStart)
            toogleCount--;
    }

    /// <summary> Gate should block path again </summary>
    public void EnableCable()
    {
        if(isActiveOnStart)
            toogleCount++;

        if(toogleCount == 0)
        {
            animator.SetBool("IsActivated", true);
        }

        if(!isActiveOnStart)
            toogleCount++;
    }
}
