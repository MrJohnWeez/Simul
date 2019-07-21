using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1FinishedLevel3 : MonoBehaviour
{
    public Level3Script level3Script = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            level3Script.Player1Finished = true;
        }
        level3Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            level3Script.Player1Finished = false;
        }
    }
}
