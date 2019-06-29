using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1FinishedTutorial : MonoBehaviour
{
    public TutorialScript tutorialScript = null;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            tutorialScript.Player1Finished = true;
        }
        tutorialScript.CheckIfPlayersAreFinished();
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Failed");
        if(other.tag == "Player")
        {
            tutorialScript.Player1Finished = false;
        }
    }
}
