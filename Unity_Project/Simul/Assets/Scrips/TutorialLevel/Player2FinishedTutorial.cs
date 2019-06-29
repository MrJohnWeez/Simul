using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2FinishedTutorial : MonoBehaviour
{
    public TutorialScript tutorialScript = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            tutorialScript.Player2Finished = true;
        }
        tutorialScript.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            tutorialScript.Player2Finished = false;
        }
    }
}
