using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Trigger : MonoBehaviour
{
    public Level3Script level3Script = null;
    public static int playersAtEnd = 0;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            playersAtEnd++;
        }
        level3Script.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            playersAtEnd--;
        }
    }
}
