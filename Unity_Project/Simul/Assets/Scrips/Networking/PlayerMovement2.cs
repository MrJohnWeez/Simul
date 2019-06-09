using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618 // disable network obsolete warning
public class PlayerMovement2 : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update runs on all clients
    void Update()
    {
       
        if (!hasAuthority)
        {
            return;
        }

         // client owns local object
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(0,-1,0);
        }
    }
}
