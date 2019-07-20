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
        PlayerPrefs.SetInt("level1Unlocked", 1);
        PlayerPrefs.SetInt("level2Unlocked", 1);
        PlayerPrefs.SetInt("level3Unlocked", 1);
        MainMenu.forceAboutMenu = true;
        sceneController.MainMenu();
    }

    public override void RestartLevel()
    {
        sceneController.Level3SinglePlayer();
    }
}
