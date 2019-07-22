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

            if(Input.touchCount > 0)
            {
                if(Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else
            {
                if(Cursor.lockState != CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            
        #endif
    }
}
