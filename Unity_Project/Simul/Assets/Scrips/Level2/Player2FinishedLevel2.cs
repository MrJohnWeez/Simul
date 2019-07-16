using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2FinishedLevel2 : MonoBehaviour
{
    public Level2Script level2Script = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            level2Script.Player2Finished = true;
        }
        level2Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            level2Script.Player2Finished = false;
        }
    }
}
