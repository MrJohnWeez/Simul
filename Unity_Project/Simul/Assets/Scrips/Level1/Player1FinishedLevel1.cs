using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1FinishedLevel1 : MonoBehaviour
{
    public Level1Script level1Script = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            level1Script.Player1Finished = true;
        }
        level1Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Failed");
        if(other.tag == "Player")
        {
            level1Script.Player1Finished = false;
        }
    }
}
