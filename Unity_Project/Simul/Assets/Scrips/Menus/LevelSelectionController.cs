using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Controls the enitre animated menu cutscene and normal level selection </summary>
public class LevelSelectionController : MonoBehaviour
{
    /// <summary> Set to true before loading scene to make scene only animate and go to next level </summary>
    public static bool AnimateNewLevel = false;
    public Color lockedColor;
    public Color unlockedColor;
    public float animationSpeed = 2;
    public Image[] paths = null;
    public GameObject[] levelButtons = null;
    public Image[] levelBackgrounds = null;
    public GameObject backButton = null;
    public SceneController sceneController = null;

    private bool[] _levelsUnlocked = new bool[4];
    private int _firstLockedLevel = 4;

    private void Start()
    {
        _levelsUnlocked[0] = true;
        _levelsUnlocked[1] = PlayerPrefs.GetInt("level1Unlocked")==1?true:false;
        _levelsUnlocked[2] = PlayerPrefs.GetInt("level2Unlocked")==1?true:false;
        _levelsUnlocked[3] = PlayerPrefs.GetInt("level3Unlocked")==1?true:false;

        // Disable all buttons to levels not yet unlocked and color them accordnely
        for(int i = 0; i < 4; i++)
        {            
            if(_levelsUnlocked[i])
            {
                levelButtons[i].SetActive(true);
                levelBackgrounds[i].color = unlockedColor;
            }
            else
            {
                if(_firstLockedLevel == 4)
                {
                    _firstLockedLevel = i;
                }
                levelButtons[i].SetActive(false);
                levelBackgrounds[i].color = lockedColor;
            }
        }
        
        // If scene should only animate disable all interactions and setup assets for animation
        if(AnimateNewLevel && _firstLockedLevel != 1)
        {
            backButton.SetActive(false);
            foreach(GameObject go in levelButtons)
            {
                go.SetActive(false);
            }

            levelButtons[_firstLockedLevel-1].SetActive(false);
            levelBackgrounds[_firstLockedLevel-1].color = lockedColor;
            SetPathsUpToLevel(_firstLockedLevel-1);
            AnimateToLevel(_firstLockedLevel-1);
        }
        else
        {
            // If not animating show the paths to the latest unlocked level
            SetPathsUpToLevel(_firstLockedLevel);
        }
    }

    /// <summary> Show all paths up until level that will be animated </summary>
    private void SetPathsUpToLevel(int levelToKeepLocked)
    {
        HideAllPaths();

        if(levelToKeepLocked > 1)
        {
            paths[0].fillAmount = 1;
            paths[1].fillAmount = 1;
        }

        if(levelToKeepLocked > 2)
        {
            paths[2].fillAmount = 1;
            paths[3].fillAmount = 1;
        }

        if(levelToKeepLocked > 3)
        {
            paths[4].fillAmount = 1;
            paths[5].fillAmount = 1;
        }
    }

    /// <summary> Hide all path assets on the menu </summary>
    private void HideAllPaths()
    {
        foreach(Image i in paths)
        {
            i.fillAmount = 0;
        }
    }
    private void AnimateToLevel(int levelValue)
    {
        StartCoroutine(ToLevel(levelValue));
    }

    /// <summary> Animate the map paths to unlock the given level </summary>
    private IEnumerator ToLevel(int levelValue)
    {
        if(levelValue == 1)
        {
            while(paths[0].fillAmount < 1)
            {
                paths[0].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }

            levelBackgrounds[levelValue].color = unlockedColor;

            while(paths[1].fillAmount < 1)
            {
                paths[1].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }
            sceneController.Level1SinglePlayer();
            Debug.Log("Go to level 2 now!");
        }
        else if(levelValue == 2)
        {
            while(paths[2].fillAmount < 1)
            {
                paths[2].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }

            levelBackgrounds[levelValue].color = unlockedColor;

            while(paths[3].fillAmount < 1)
            {
                paths[3].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }
            sceneController.Level2SinglePlayer();
            Debug.Log("Go to level 3 now!");
        }
        else if(levelValue == 3)
        {
            while(paths[4].fillAmount < 1)
            {
                paths[4].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }

            levelBackgrounds[levelValue].color = unlockedColor;

            while(paths[5].fillAmount < 1)
            {
                paths[5].fillAmount += Time.deltaTime * animationSpeed;
                yield return 0;
            }
            sceneController.Level3SinglePlayer();
            Debug.Log("Go to level 4 now!");
        }
        AnimateNewLevel = false;
    }
}
