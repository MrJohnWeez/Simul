using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLogCam : MonoBehaviour
{
    public TutorialScript tutorialScript = null;
    private bool hasEntered = false;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            tutorialScript.LogCutscene();
        }
    }
}
