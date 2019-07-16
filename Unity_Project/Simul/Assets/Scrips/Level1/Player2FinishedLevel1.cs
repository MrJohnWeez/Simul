using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2FinishedLevel1 : MonoBehaviour
{
    public Level1Script level1Script = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            level1Script.Player2Finished = true;
        }
        level1Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            level1Script.Player2Finished = false;
        }
    }
}
