using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows user to switch player control in single player </summary>
public class PlayerSwitcher : MonoBehaviour
{
    public GameObject firstPlayer = null;
    public GameObject secondPlayer = null;
    public LayerMask player1Culling;
    public LayerMask player2Culling;
    public SceneController sceneController = null;

    private PlayerController firstPC = null;
    private PlayerController secondPC = null;

    private bool isFirstPlayer = true;
    private Camera mainCam = null;
    

    void Awake()
    {
        firstPC = firstPlayer.GetComponent<PlayerController>();
        secondPC = secondPlayer.GetComponent<PlayerController>();
        mainCam = GetComponent<Camera>();
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
        //Debug.Log("Update: " + isFirstPlayer + "    " + firstPC.isActive + "     " + secondPC.isActive);
        isFirstPlayer = !isFirstPlayer;
        UpdateActive(isFirstPlayer);
    }

    /// <summary> Control what player user can control </summary>
    public void UpdateActive(bool firstIsActive)
    {
        if(firstIsActive)
        {
            mainCam.cullingMask = player1Culling;
        }
        else
        {
            mainCam.cullingMask = player2Culling;
        }
        firstPC.isActive = firstIsActive;
        secondPC.isActive = !firstIsActive;
    }

    public void DisableAllPlayers()
    {
        firstPC.Disable();
        secondPC.Disable();
    }
}
