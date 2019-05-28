using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
#pragma warning disable 0618 // disable network obsolete warning

public class PlayerConnection : NetworkBehaviour
{
    public GameObject playerPrefab;
    private GameObject myUnit;
    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            // object belongs to another player
            return;
        }
        CmdSpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /////// Commands
    [Command]
    private void CmdSpawnUnit()
    {
        // On server right now
        GameObject go = Instantiate(playerPrefab);
        
        myUnit = go;
        // alert all clients of object and create network id
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
