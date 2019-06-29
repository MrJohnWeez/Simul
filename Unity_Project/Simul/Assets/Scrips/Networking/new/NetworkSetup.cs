using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma warning disable 0618 // disable network obsolete warning

public class NetworkSetup : MonoBehaviour
{
    public GameObject HostOrJoinMenu = null;
    public GameObject HostWait = null;
    public GameObject JoinWait = null;
    public NetworkedManager NetManager = null;
    public Text hostedRoomText = null;
    public Text clientRoomText = null;
    private string hostedRoom = "";
    private string clientIP = "";
    private string clientPort = "";


    // Start is called before the first frame update
    void Start()
    {
        HostOrJoinMenu.SetActive(true);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
        UpdateClientRoom();
    }


    public void UpdateClientRoom()
    {
        string[] ipAndPort = clientRoomText.text.Split(':');
        if(ipAndPort.Length == 2)
        {
            clientIP = ipAndPort[0];
            clientPort = ipAndPort[1];
        }
    }
    
    public void OpenJoinMenu()
    {
        JoinMenu();
    }
    public void AttemptJoin()
    {
        if(NetManager.isClient)
        {
            NetManager.StopClient();
            NetManager.isClient = true;
        }

        if(clientIP.Length > 0 && clientPort.Length > 0)
        {
            NetManager.networkAddress = clientIP;
            try
            {
                NetManager.networkPort = int.Parse(clientPort);
            }
            catch
            {
                return;
            }
            NetManager.isClient = true;
            NetManager.StartClient();
        }
        
    }

    public void AttemptHost()
    {

        NetManager.StartHost();
        if(NetManager.isNetworkActive)
        {
            string ipv4 = IPManager.GetIP(ADDRESSFAM.IPv4);
            hostedRoom = ipv4 + ":" + NetManager.networkPort;
            hostedRoomText.text = hostedRoom;
            HostWaitMenu();
        }
    }

    public void HostWaitMenu()
    {
        HostOrJoinMenu.SetActive(false);
        HostWait.SetActive(true);
        JoinWait.SetActive(false);
    }

    public void HostJoinMenu()
    {
        HostOrJoinMenu.SetActive(true);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
    }

    public void JoinMenu()
    {
        HostOrJoinMenu.SetActive(false);
        HostWait.SetActive(false);
        JoinWait.SetActive(true);
    }

    public void CancelHost()
    {
        NetManager.StopHost();
        HostJoinMenu();
    }

    public void CancelJoin()
    {
        NetManager.StopClient();
        HostJoinMenu();
        NetManager.isClient = false;
    }

    public void CloseNetwork()
    {
        if(NetManager.isClient)
        {
            CancelJoin();
        }
        else
        {
            CancelHost();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GameStarted()
    {
        HostOrJoinMenu.SetActive(false);
        HostWait.SetActive(false);
        JoinWait.SetActive(false);
    }
}
