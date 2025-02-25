﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Script : BaseLevel
{
    protected override void Start() {
        base.Start();
        Level3Trigger.playersAtEnd = 0;
        SettingsController.UserInput = true;
        SettingsController.IsPaused = false;
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

    public override void CheckIfPlayersAreFinished()
    {
        if(Level3Trigger.playersAtEnd >= 2 && !finishedLevel)
        {
            Level3Trigger.playersAtEnd = 0;
            finishedLevel = true;
            FinishedLevel();
        }
    }

    public override void RestartLevel()
    {
        SettingsController.UserInput = true;
        SettingsController.IsPaused = false;
        Level3Trigger.playersAtEnd = 0;
        sceneController.Level3SinglePlayer();
        
    }
}
