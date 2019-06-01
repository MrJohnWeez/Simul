using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetManager : NetworkManager
{

	public MyNetworkDiscovery discovery;
	public Text debugText;
	public GameObject ViewRoom = null;
    public GameObject parentRoom = null;
	public int playerCount = 0;
	public GameObject player1;
	public GameObject player2;

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
	private void Start() {
		debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<Text>();
	}
	public override void OnStartHost()
	{
		discovery.Initialize();
		discovery.StartAsServer();
		debugText.text += "\nOnStartHost";
	}

	public override void OnClientConnect(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		Debug.Log("Adding Player: " + GetConnectionCount());
		if(GetConnectionCount() <= 1)
		{
			GameObject player = (GameObject)Instantiate(player1, Vector3.zero, Quaternion.identity);
        	NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
		else
		{
			GameObject player = (GameObject)Instantiate(player2, Vector3.zero, Quaternion.identity);
        	NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
    }

	// public void SpawnRoom(string fromAddress, string data)
	// {
	// 	GameObject test = GameObject.Instantiate(ViewRoom,parentRoom.transform);
    //     ConnectToServerButton cb = test.GetComponentInChildren<ConnectToServerButton>();
    //     cb.UpdateRoom(fromAddress, data);
	// }

	public override void OnStartClient(NetworkClient client)
	{
		discovery.showGUI = false;
		debugText.text += "\nOnStartClient";
	}

	public override void OnStopClient()
	{
		discovery.StopBroadcast();
		discovery.showGUI = true;
		debugText.text += "\nOnStopClient";
	}
}
