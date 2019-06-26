using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
#pragma warning disable 0618 // disable network obsolete warning
public class TestNetworking : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Received broadcast from: " + fromAddress+ " with the data: " + data);
    }
}
