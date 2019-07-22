using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevel : MonoBehaviour
{
    [System.NonSerialized]
    public bool Player1Finished = false;
    [System.NonSerialized]
    public bool Player2Finished = false;
    public PlayerSwitcher playerSwitcher = null;
    public InGameMenu inGameMenu = null;
    protected GameObject mainCam = null;
    protected bool finishedLevel = false;
    protected Coroutine currentCoroutine = null;
    public SceneController sceneController = null;

    protected virtual void Start() {
        mainCam = Camera.main.gameObject;
    }

    /// <summary> Check if both players are standing in the end areas </summary>
    public virtual void CheckIfPlayersAreFinished()
    {
        if(Player1Finished && Player2Finished && !finishedLevel)
        {
            finishedLevel = true;
            FinishedLevel();
        }
    }


    private void Update() {
        if(SettingsController.UserInput && InputHandler.instance.GetButtonDown("UpButton"))
        {
            RestartLevel();
        }
    }

    public abstract void FinishedLevel();
    public abstract void RestartLevel();
}
