using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
#pragma warning disable 0618 // disable network obsolete warning
public class NetworkedManager : NetworkManager
{
    public GameObject playerObjectPrefab;
    public GameObject player1World;
	public GameObject player2World;
	public GameObject player1Spawn;
	public GameObject player2Spawn;
    public NetworkSetup networkSetup;

    private GameObject playerObject = null;
    public bool isClient = false;

    int GetConnectionCount()
    {
        int count = 0;
        foreach (NetworkConnection con in NetworkServer.connections)
        {
            if (con != null)
                count++;
        }
        return count;
    }

	public void StopAll()
	{
		if (NetworkServer.active || IsClientConnected())
		{
			StopHost();
		}
		Destroy(gameObject);
	}

	public override void OnClientConnect(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
        Debug.Log("conn: " + conn);
        if(GetConnectionCount() == 2 || isClient)
        {
            Debug.Log("Go to game now!");
            networkSetup.GameStarted();
        }
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		Debug.Log("Adding Player: " + GetConnectionCount());
		if(GetConnectionCount() <= 1)
		{
			playerObject = (GameObject)Instantiate(playerObjectPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
			playerObject.transform.parent = player1World.transform;
        	NetworkServer.AddPlayerForConnection(conn, playerObject, playerControllerId);
		}
		else
		{
			playerObject = (GameObject)Instantiate(playerObjectPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
			playerObject.transform.parent = player2World.transform;
        	NetworkServer.AddPlayerForConnection(conn, playerObject, playerControllerId);
		}
        if(GetConnectionCount() == 2)
        {
            Debug.Log("Go to game now!");
            networkSetup.GameStarted();
        }
    }
}
