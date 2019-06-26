using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helper class for all buttons to call high level netowrk fucntions
public class HighLevelNetworkControl : MonoBehaviour
{
    public MyNetManager networkManager = null;
    private void Start() {
        if(!networkManager)
        {
            networkManager = GameObject.FindObjectOfType<MyNetManager>();
        }
    }

    public void StartHost()
    {
        networkManager.StartHostButton();
    }

    public void StartClient()
    {
        networkManager.StartClientButton();
    }


    public void QuitAllNetworking()
    {
        networkManager.StopAll();
    }

}
