using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
#pragma warning disable 0618 // disable network obsolete warning
public class MyNetManager : NetworkManager
{

	public Text debugText;
	public GameObject ViewRoom = null;
    public GameObject parentRoom = null;
	public int playerCount = 0;
	public GameObject player1;
	public GameObject player2;
	public GameObject player1World;
	public GameObject player2World;
	public GameObject player1Spawn;
	public GameObject player2Spawn;

	private GameObject playerObject = null;
	public GameObject joinMenu = null;
	public GameObject InGameHud = null;

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
		joinMenu.SetActive(true);
		InGameHud.SetActive(false);
		debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<Text>();
	}

	public void StartHostButton()
	{
		StartHost();
		debugText.text += "\nOnStartHost";
		joinMenu.SetActive(false);
	}

	public void StopAll()
	{
		if (NetworkServer.active || IsClientConnected())
		{
			StopHost();
		}
		Destroy(gameObject);
	}

	public void StartClientButton()
	{
		joinMenu.SetActive(false);
		Debug.Log("Started Client");
	}


	public override void OnStartHost()
	{
		// discovery.Initialize();
		// discovery.StartAsServer();
		// debugText.text += "\nOnStartHost";
	}

	public override void OnClientConnect(NetworkConnection conn) {
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		
		Debug.Log("Adding Player: " + GetConnectionCount());
		if(GetConnectionCount() <= 1)
		{
			playerObject = (GameObject)Instantiate(player1, player1Spawn.transform.position, player1Spawn.transform.rotation);
			playerObject.transform.parent = player1World.transform;
        	NetworkServer.AddPlayerForConnection(conn, playerObject, playerControllerId);
		}
		else
		{
			playerObject = (GameObject)Instantiate(player2, player2Spawn.transform.position, player2Spawn.transform.rotation);
			playerObject.transform.parent = player2World.transform;
        	NetworkServer.AddPlayerForConnection(conn, playerObject, playerControllerId);
		}
    }

	
	

	public void JoinGame(string fromAddress, string data)
	{
		InGameHud.SetActive(true);
		networkAddress = fromAddress;
        StartClient();
		//discovery.showGUI = false;
		debugText.text += "\nOnStartClient";
	}

	// public override void OnStartClient(NetworkClient client)
	// {
	// 	discovery.showGUI = false;
	// 	debugText.text += "\nOnStartClient";
	// }

	public override void OnStopClient()
	{
		joinMenu.SetActive(true);
		InGameHud.SetActive(false);
		MyNetworkDiscovery.hasRecievedBroadcastAtLeastOnce = false;
		debugText.text += "\nOnStopClient";
	}
}
