using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Used to load different scenes within our game </summary>
public class SceneController: MonoBehaviour
{
    public void Level1SinglePlayer()
    {
        SceneManager.LoadScene("Level1SinglePlayer", LoadSceneMode.Single);
    }

    public void Level1Multiplayer()
    {
        SceneManager.LoadScene("Level1Networked", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void About()
    {
        Debug.Log("Go to about menu");
    }
    
    /// <summary> Quit the game or stop playing if in editor </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
