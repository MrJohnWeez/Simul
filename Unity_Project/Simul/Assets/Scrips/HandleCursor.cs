using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCursor : MonoBehaviour
{
    void Update()
    {
        #if UNITY_STANDALONE
            if(Cursor.visible)
            {
                Cursor.visible = false;
            }
            if(Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        #endif
    }
}
