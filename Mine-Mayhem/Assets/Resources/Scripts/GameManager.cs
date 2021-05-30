using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static CustomPlayerController Player
    {
        get
        {
            CustomPlayerController player = UnityEngine.Object.FindObjectOfType<CustomPlayerController>();
            if (player != null)
            {
                return player;
            }
            else
            {
                return null;
            }
        }
    }

    public static int LevelIndex { get; private set; }

    // Star 1 is for Level Complete (Gold Collection)
    public static bool Star1 { get; private set; }

    // Star 2 is for all GEMS collected.
    public static bool Star2 { get; private set; }

    // Star 3 is for completing level with health at / above %60.
    public static bool Star3 { get; private set; }
    public static bool[] StarConditions { get; private set; }
    public static int StarsAcquired { get; private set; }

    public static List<Collectible> GemsInCurrentLevel;


    private static bool isPaused = false;
    private static bool hpOn = false;

    public static event Action<bool> OnPause;
    public static event Action<bool> OnToggleHP;
    public static event Action OnLevelStart;
    public static event Action<bool> OnToggleDeathPanel;
    public static event Action<bool> OnToggleSuccessPanel;
    

    static GameManager()
    {
        StarConditions = new bool[]
        {
            Star1 = false,
            Star2 = false,
            Star3 = false
        };

        GemsInCurrentLevel = new List<Collectible>();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        MM_UI.OnRetry += UI_OnRetry;
        MM_UI.OnMainMenu += UI_OnMainMenu;
        MM_UI.OnQuit += UI_OnQuit;
        Collectible.OnPickUpCollectible += Collectible_OnPickUp;
        CustomPlayerController.OnPlayerIsDead += Player_OnPlayerIsDead;
    }

    private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // When a scene loads, store the levels build index in 'LevelIndex'.
        if (LevelIndex != arg0.buildIndex)
        {
            LevelIndex = arg0.buildIndex;
            StarsAcquired = 0;
        }

        // When a scene loads and the scene is not the Main Menu, handle certain elements.
        if (arg0.name != LevelInformation.Levels[0].name)
        {
            if (!hpOn)
            {
                hpOn = !hpOn;
            }

            Debug.Log("HP bar is now turned on.");
            OnLevelStart?.Invoke();

            #region WHEN SCENE STARTS, CLEAR LIST OF COLLECTIBLES AND FIND ALL NEW ONES IN CURRENT SCENE LOADED
            // On Scene Loaded, the new level gold has not yet been acquired.
            for(int i = 0; i < StarConditions.Length; i++)
            {
                if (StarConditions[i])
                    StarConditions[i] = false;
            }

            if(GemsInCurrentLevel.Count > 0)
            {
                GemsInCurrentLevel.Clear();
            }

            // Gather list of all GEMS in the current loaded scene.
            Collectible[] collectibles = UnityEngine.Object.FindObjectsOfType<Collectible>();
            foreach(Collectible collectible in collectibles)
            {
                if(collectible.Type == Collectible.CollectibleType.GEM)
                {
                    if (!GemsInCurrentLevel.Contains(collectible))
                    {
                        //Debug.Log(collectible);
                        GemsInCurrentLevel.Add(collectible);
                    }
                }
            }
            #endregion
        }
        else
        {
            if (hpOn)
            {
                hpOn = !hpOn;
            }

        }

        OnToggleHP?.Invoke(hpOn);
    }

    public static void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByBuildIndex(0).buildIndex)
            {
                isPaused = !isPaused;
                TogglePause(isPaused);
            }
        }
    }

    private static void UI_OnRetry()
    {
        Debug.Log("This is 'UI_OnRetry' contained in GameManager class.");

        TogglePause(false);
        Player_OnPlayerIsDead(false);
        OnToggleSuccessPanel?.Invoke(false);

        SceneManager.LoadScene(LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex].name);
    }

    private static void UI_OnMainMenu()
    {
        //Debug.Log("This is the 'UI_OnMainMenu' method contained in the GameManager class.");

        TogglePause(false);
        Player_OnPlayerIsDead(false);
        OnToggleSuccessPanel?.Invoke(false);

        SceneManager.LoadScene(LevelInformation.Levels[0].name);
    }

    private static void UI_OnQuit()
    {
        Debug.Log("This is the 'UI_OnQuit' method contained in the GameManager class.");

        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    private static void Collectible_OnPickUp(Collectible collectible, Collectible.CollectibleType pickupType)
    {
        //Debug.Log("This is the pick up function on GameManager.");
        switch (pickupType)
        {
            case Collectible.CollectibleType.GOLD:
                Debug.Log($"Just found the gold!!");
                if (!StarConditions[0])
                {
                    StarConditions[0] = true;
                }
                break;

            case Collectible.CollectibleType.GEM:
                Debug.Log($"Just picked up a GEM!.");
                if (GemsInCurrentLevel.Contains(collectible))
                {
                    GemsInCurrentLevel.Remove(collectible);
                }

                if(GemsInCurrentLevel.Count == 0)
                {
                    StarConditions[1] = true;
                }
                break;
        }

        // This is what happens when the gold has been acquired. (Handle player success)
        if (StarConditions[0])
        {
            Player.SetState(CustomPlayerController.PlayerStates.SUCCESS);

            // When the level is complete but the level did not contain any GEMS, the star condition is automatically fulfilled.
            if(GemsInCurrentLevel.Count == 0 && !StarConditions[1])
            {
                StarConditions[1] = true;
            }

            if((Player.curPlayerHealth / Player.maxPlayerHealth) >= 0.6f)
            {
                StarConditions[2] = true;
            }

            for(int i = 0; i < StarConditions.Length; i++)
            {
                if (StarConditions[i])
                {
                    StarsAcquired++;
                }
            }

            // Assign the awarded stars to the level and unlock the next level.
            LevelInformation.Levels[LevelIndex].stars = (Level.LevelStars)StarsAcquired;
            if(LevelInformation.Levels[LevelIndex + 1].levelLocked)
            {
                LevelInformation.Levels[LevelIndex + 1].levelLocked = false;
            }

            SaveSystem.SaveGame();

            OnToggleSuccessPanel?.Invoke(true);
        }
    }

    private static void Player_OnPlayerIsDead(bool value)
    {
        //Debug.Log($"Dead: {value}. - Now toggling the dead menu.");
        OnToggleDeathPanel?.Invoke(value);
    }

    private static void TogglePause(bool value)
    {
        if(isPaused != value)
        {
            isPaused = value;
        }
        Time.timeScale = isPaused ? 0 : 1;
        Player.DisablePlayer(isPaused);

        OnPause?.Invoke(isPaused);
    }
}
