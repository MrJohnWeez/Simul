using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public bool Player1Finished = false;
    public bool Player2Finished = false;
    public PlayerSwitcher playerSwitcher = null;
    public GameObject RopeBreakInstructions = null;
    public GameObject ChallengeInstructions = null;
    public Transform RokeBreakCam = null;
    public Transform RokeBreakCam2 = null;
    public Transform OverviewMapCam = null;
    public Transform PlayerCamera = null;
    private GameObject mainCam = null;
    private Coroutine currentCoroutine = null;
    public SceneController sceneController = null;
    private bool finishedLevel = false;

    void Start()
    {
        mainCam = Camera.main.gameObject;
        playerSwitcher.DisableAllPlayers();
        RopeBreakCutscene();
    }

    public void CheckIfPlayersAreFinished()
    {
        if(Player1Finished && Player2Finished && !finishedLevel)
        {
            finishedLevel = true;
            FinishedTutorial();
        }
    }

    public void FinishedTutorial()
    {
        sceneController.Level1SinglePlayer();
    }
    public void RopeBreakCutscene()
    {
        RopeBreakInstructions.SetActive(true);
        ChallengeInstructions.SetActive(false);
        mainCam.transform.position = RokeBreakCam.position;
        mainCam.transform.rotation = RokeBreakCam.rotation;
        currentCoroutine = StartCoroutine(MoveToRope());
        
    }

    public void OverviewCutscene()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        RopeBreakInstructions.SetActive(false);
        ChallengeInstructions.SetActive(true);
        currentCoroutine = StartCoroutine(MoveToOverview());
    }

    IEnumerator MoveToRope()
    {
        Quaternion orgRot = mainCam.transform.rotation;
        Vector3 orgPos = mainCam.transform.position;
        float timer = 0.0f;
        while(timer < 1)
        {
            mainCam.transform.position = Vector3.Lerp(orgPos, RokeBreakCam2.position, timer);
            mainCam.transform.rotation = Quaternion.Lerp(orgRot,RokeBreakCam2.rotation,timer);
            timer += Time.deltaTime/20;
            yield return 0;
        }
    }

    IEnumerator MoveToOverview()
    {
        Quaternion orgRot = mainCam.transform.rotation;
        Vector3 orgPos = mainCam.transform.position;
        float timer = 0.0f;
        while(timer < 1)
        {
            mainCam.transform.position = Vector3.Lerp(orgPos, OverviewMapCam.position, timer);
            mainCam.transform.rotation = Quaternion.Lerp(orgRot,OverviewMapCam.rotation,timer);
            timer += Time.deltaTime/5;
            yield return 0;
        }
    }

    IEnumerator MoveToPlayer()
    {
        Quaternion orgRot = mainCam.transform.rotation;
        Vector3 orgPos = mainCam.transform.position;
        float timer = 0.0f;
        while(timer < 1)
        {
            mainCam.transform.position = Vector3.Lerp(orgPos, PlayerCamera.position, timer);
            mainCam.transform.rotation = Quaternion.Lerp(orgRot,PlayerCamera.rotation,timer);
            timer += Time.deltaTime/2;
            yield return 0;
        }
        playerSwitcher.UpdateActive(true);
    }

    public void StartGame()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        RopeBreakInstructions.SetActive(false);
        ChallengeInstructions.SetActive(false);
        StartCoroutine(MoveToPlayer());
    }
}
