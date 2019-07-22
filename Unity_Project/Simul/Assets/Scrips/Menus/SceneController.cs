using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Used to load different scenes within our game </summary>
public class SceneController: MonoBehaviour
{
    public void Level3SinglePlayer()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level2SinglePlayer()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level1SinglePlayer()
    {
        SceneManager.LoadScene("Level1");
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
