using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Allows user to switch player control in single player </summary>
public class PlayerSwitcher : MonoBehaviour
{
    public GameObject firstPlayer = null;
    public GameObject secondPlayer = null;
    public GameObject firstPlayerCamera = null;
    public GameObject secondPlayerCamera = null;
    public SceneController sceneController = null;

    private PlayerController firstPC = null;
    private PlayerController2 secondPC = null;

    private Camera mainCam = null;
    private bool isFirstPlayer = true;

    void Start()
    {
        mainCam = Camera.main;
        firstPC = firstPlayer.GetComponent<PlayerController>();
        secondPC = secondPlayer.GetComponent<PlayerController2>();
        UpdateActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePlayer();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            sceneController.MainMenu();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            sceneController.Level1SinglePlayer();
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

    private void LateUpdate() {
        // Set the camera position to follow the correct player
        if(isFirstPlayer)
        {
            mainCam.transform.SetPositionAndRotation(firstPlayerCamera.transform.position, firstPlayerCamera.transform.rotation);
        }
        else
        {
            mainCam.transform.SetPositionAndRotation(secondPlayerCamera.transform.position, secondPlayerCamera.transform.rotation);
        }
    }
}
