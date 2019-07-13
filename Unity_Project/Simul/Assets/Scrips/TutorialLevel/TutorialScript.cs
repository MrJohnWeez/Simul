using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [System.NonSerialized]
    public bool Player1Finished = false;
    [System.NonSerialized]
    public bool Player2Finished = false;
    public PlayerSwitcher playerSwitcher = null;
    public GameObject RopeBreakInstructions = null;
    public GameObject ChallengeInstructions = null;
    public GameObject LogInstuctions = null;
    public GameObject SwitchPlayersInstructions = null;
    public GameObject EndTutorial = null;
    public Transform RokeBreakCam = null;
    public Transform RokeBreakCam2 = null;
    public Transform OverviewMapCam = null;
    public Transform LogCam = null;
    public Transform SwitchPlayerCam = null;
    public Transform EndTutorialCam = null;
    public Transform PlayerCamera = null;
    private GameObject mainCam = null;
    private Coroutine currentCoroutine = null;
    public SceneController sceneController = null;
    public InGameMenu inGameMenu = null;
    private bool finishedLevel = false;

    void Start()
    {
        mainCam = Camera.main.gameObject;
        playerSwitcher.DisableAllPlayers();
        RopeBreakCutscene();
    }

    /// <summary> Check if both players are standing in the end areas </summary>
    public void CheckIfPlayersAreFinished()
    {
        if(Player1Finished && Player2Finished && !finishedLevel)
        {
            finishedLevel = true;
            FinishedTutorial();
        }
    }

    private void Update() {
        if(SettingsController.UserInput && InputHandler.instance.GetButtonDown("UpButton"))
        {
            RestartLevel();
        }
    }
    public void RestartLevel()
    {
        sceneController.TutorialLevel();
    }

    /// <summary> Finish the level and go to the map cutscene or normal level selection </summary>
    public void FinishedTutorial()
    {
        if(PlayerPrefs.GetInt("level1Unlocked")==1?true:false)
        {
            sceneController.LevelSelection();
        }
        else
        {
            PlayerPrefs.SetInt("level1Unlocked", 1);
            LevelSelectionController.AnimateNewLevel = true;
            sceneController.LevelSelection();
        }
    }


    public void RopeBreakCutscene()
    {
        DismissMenus();
        SettingsController.UserInput = false;
        RopeBreakInstructions.SetActive(true);
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
        DismissMenus();
        SettingsController.UserInput = false;
        ChallengeInstructions.SetActive(true);
        currentCoroutine = StartCoroutine(MoveToOverview());
    }

    public void LogCutscene()
    {
        DismissMenus();
        SettingsController.UserInput = false;
        playerSwitcher.DisableAllPlayers();
        LogInstuctions.SetActive(true);
        mainCam.transform.position = LogCam.position;
        mainCam.transform.rotation = LogCam.rotation;
    }

    public void SwitchPlayerCutscene()
    {
        DismissMenus();
        SettingsController.UserInput = false;
        playerSwitcher.DisableAllPlayers();
        SwitchPlayersInstructions.SetActive(true);
        mainCam.transform.position = SwitchPlayerCam.position;
        mainCam.transform.rotation = SwitchPlayerCam.rotation;
    }

    public void EndTutorialCutscene()
    {
        DismissMenus();
        SettingsController.UserInput = false;
        playerSwitcher.DisableAllPlayers();
        EndTutorial.SetActive(true);
        mainCam.transform.position = EndTutorialCam.position;
        mainCam.transform.rotation = EndTutorialCam.rotation;
    }

    IEnumerator MoveToRope()
    {
        SettingsController.UserInput = false;
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
        SettingsController.UserInput = false;
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
        SettingsController.UserInput = false;
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
        DismissMenus(true);
        inGameMenu.OpenControlsMenu();
    }

    /// <summary> Closes all menus and gives the player control again if entered true </summary>
    public void DismissMenus(bool SetPlayerActive = false)
    {
        if(SetPlayerActive)
        {
            playerSwitcher.UpdateActive(true);
        }
        RopeBreakInstructions.SetActive(false);
        ChallengeInstructions.SetActive(false);
        LogInstuctions.SetActive(false);
        SwitchPlayersInstructions.SetActive(false);
        EndTutorial.SetActive(false);
        SettingsController.UserInput = true;
    }

    /// <summary> Start the tutorial level and give the user control of their character </summary>
    public void StartGame()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        DismissMenus();
        StartCoroutine(MoveToPlayer());
    }
}
