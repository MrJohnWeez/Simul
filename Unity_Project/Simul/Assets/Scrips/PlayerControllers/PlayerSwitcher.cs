using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows user to switch player control in single player </summary>
public class PlayerSwitcher : MonoBehaviour
{
    public GameObject firstPlayer = null;
    public GameObject secondPlayer = null;
    public SceneController sceneController = null;

    private PlayerController firstPC = null;
    private PlayerController secondPC = null;

    private bool isFirstPlayer = true;

    void Awake()
    {
        firstPC = firstPlayer.GetComponent<PlayerController>();
        secondPC = secondPlayer.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(SettingsController.UserInput && InputHandler.instance.GetButtonDown("LeftButton"))
        {
            TogglePlayer();
        }
    }

    /// <summary> Toggles what player the user controls </summary>
    public void TogglePlayer()
    {
        isFirstPlayer = !isFirstPlayer;
        UpdateActive(isFirstPlayer);
    }

    /// <summary> Control what player user can control </summary>
    public void UpdateActive(bool firstIsActive)
    {
        firstPC.isActive = firstIsActive;
        secondPC.isActive = !firstIsActive;
    }

    public void DisableAllPlayers()
    {
        firstPC.Disable();
        secondPC.Disable();
    }
}
