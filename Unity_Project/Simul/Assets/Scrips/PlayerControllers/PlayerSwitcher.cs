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

    void Start()
    {
        firstPC = firstPlayer.GetComponent<PlayerController>();
        secondPC = secondPlayer.GetComponent<PlayerController>();
        UpdateActive(true);
    }

    void Update()
    {
        if(!SettingsController.IsPaused)
        {
            if(InputHandler.instance.GetButtonDown("LeftButton"))
            {
                TogglePlayer();
            }

            if(InputHandler.instance.GetButtonDown("UpButton"))
            {
                sceneController.Level1SinglePlayer();
            }
        }
    }

    /// <summary> Toggles what player the user controls </summary>
    private void TogglePlayer()
    {
        isFirstPlayer = !isFirstPlayer;
        UpdateActive(isFirstPlayer);
    }

    /// <summary> Control what player user can control </summary>
    private void UpdateActive(bool firstIsActive)
    {
        firstPC.isActive = firstIsActive;
        secondPC.isActive = !firstIsActive;
    }
}
