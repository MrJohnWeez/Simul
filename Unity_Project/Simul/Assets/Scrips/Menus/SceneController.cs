using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Used to load different scenes within our game </summary>
public class SceneController: MonoBehaviour
{
    public void Level3SinglePlayer()
    {
        SceneManager.LoadScene("TutorialLevel", LoadSceneMode.Single);
    }

    public void Level2SinglePlayer()
    {
        SceneManager.LoadScene("TutorialLevel", LoadSceneMode.Single);
    }

    public void Level1SinglePlayer()
    {
        SceneManager.LoadScene("TutorialLevel", LoadSceneMode.Single);
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene("TutorialLevel", LoadSceneMode.Single);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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
