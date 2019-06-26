using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
#pragma warning disable 0618 // disable network obsolete warning

public class NetworkSetup : MonoBehaviour
{
    public GameObject HostOrJoinMenu = null;
    public GameObject HostWait = null;
    public GameObject JoinWait = null;
    public NetworkedManager NetManager = null;
    public Text hostedRoomText = null;
    private string hostedRoom = "";
    private string clientIP = "";
    private string clientPort = "";


    // Start is called before the first frame update
    void Start()
    {
        HostOrJoinMenu.SetActive(true);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateClientRoom(Text inText)
    {
        string[] ipAndPort = inText.text.Split(':');
        if(ipAndPort.Length == 2)
        {
            clientIP = ipAndPort[0];
            clientPort = ipAndPort[1];
        }
    }
    
    public void OpenJoinMenu()
    {
        HostOrJoinMenu.SetActive(false);
        HostWait.SetActive(false);
        JoinWait.SetActive(true);
    }
    public void AttemptJoin()
    {
        NetManager.networkAddress = clientIP;
        NetManager.networkPort =  int.Parse(clientPort);
        NetManager.isClient = true;
        NetManager.StartClient();
    }

    public void AttemptHost()
    {

        NetManager.StartHost();
        if(NetManager.isNetworkActive)
        {
            string ipv4 = IPManager.GetIP(ADDRESSFAM.IPv4);
            hostedRoom = ipv4 + ":" + NetManager.networkPort;
            hostedRoomText.text = hostedRoom;
            HostOrJoinMenu.SetActive(false);
            HostWait.SetActive(true);
            JoinWait.SetActive(false);
        }
    }

    public void CancelHost()
    {
        NetManager.StopHost();
        HostOrJoinMenu.SetActive(true);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
    }

    public void CancelJoin()
    {
        HostOrJoinMenu.SetActive(true);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
        NetManager.isClient = false;
    }

    public void GameStarted()
    {
        HostOrJoinMenu.SetActive(false);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
    }
}
