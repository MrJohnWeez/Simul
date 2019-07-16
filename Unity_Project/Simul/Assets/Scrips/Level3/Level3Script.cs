using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Script : BaseLevel
{
    protected override void Start() {
        base.Start();
        playerSwitcher.UpdateActive(true);
    }


    public override void FinishedLevel()
    {
        if(PlayerPrefs.GetInt("level4Unlocked")==1?true:false)
        {
            PlayerPrefs.SetInt("level1Unlocked", 1);
            PlayerPrefs.SetInt("level2Unlocked", 1);
            PlayerPrefs.SetInt("level3Unlocked", 1);
            sceneController.LevelSelection();
        }
        else
        {
            PlayerPrefs.SetInt("level1Unlocked", 1);
            PlayerPrefs.SetInt("level2Unlocked", 1);
            PlayerPrefs.SetInt("level3Unlocked", 1);
            PlayerPrefs.SetInt("level4Unlocked", 1);
            LevelSelectionController.AnimateNewLevel = true;
            sceneController.LevelSelection();
        }
    }

    public override void RestartLevel()
    {
        sceneController.Level3SinglePlayer();
    }
}
