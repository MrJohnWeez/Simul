using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1FinishedLevel2 : MonoBehaviour
{
    public Level2Script level2Script = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            level2Script.Player1Finished = true;
        }
        level2Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Failed");
        if(other.tag == "Player")
        {
            level2Script.Player1Finished = false;
        }
    }
}
