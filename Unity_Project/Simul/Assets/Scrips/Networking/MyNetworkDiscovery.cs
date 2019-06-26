using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class MyNetworkDiscovery : NetworkDiscovery
{
    public MyNetManager networkManager;
    public Text debugText;
    public static bool hasRecievedBroadcastAtLeastOnce = false;
        private void Start() {
        networkManager = GameObject.FindObjectOfType<MyNetManager>();
        //debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<Text>();
        System.DateTime sysTime = System.DateTime.Now;
        int portValue = sysTime.Hour * 1000 + sysTime.Minute * 10 + sysTime.Millisecond % 100;
        // Debug.Log("portValue: " + portValue.ToString());
        // debugText.text += "\nportValue: " + portValue.ToString();
        // broadcastPort = portValue;
        broadcastData = "RoomNumber: " + portValue;
    }
    public override void OnReceivedBroadcast(string fromAddress, string data) {
        //debugText.text += data + "\n" + "Datat" + "\n";
        
        // deal with Unity bug, see notes above
        if (hasRecievedBroadcastAtLeastOnce) {
            debugText.text += "\navoided Unity racetrack bug";
            return;
        }
        hasRecievedBroadcastAtLeastOnce = true;
   
        Debug.Log("in client discovery. found server .. fromAddress");
        // networkManager.SpawnRoom(fromAddress, data);

        // you now want to turn off discovery
        // (as needed, it will get turned on again in YourNetworkManager#OnClientDisconnect)
   
        // intriguingly you CAN NOT DO IT HERE ... due to yet another Unity bug
        // you must do it in YourNetworkManager#OnClientConnect IMMEDIATELY after base.
        // see the notes in red in this forum post up above
        // StopBroadcast(); .. can not do this here
   
        // to start the client, simply do these two things:
        networkManager.JoinGame(fromAddress, data);
        
        // networkManager.networkAddress = fromAddress;
        // // debugText.text += "\nfromAddress: " + fromAddress;
        // networkManager.StartClient();
    }
}
