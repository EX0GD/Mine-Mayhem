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
    public static int GemsAcquired { get; private set; }

    public static List<Collectible> GemsInCurrentLevel;


    private static bool isPaused = false;
    private static bool hpOn = false;

    public static event Action<bool> OnPause;
    public static event Action<bool> OnToggleHP;
    public static event Action OnLevelStart;
    public static event Action<bool> OnToggleDeathPanel;
    public static event Action<bool> OnToggleSuccessPanel;
    public static event Action<bool> OnToggleGameFinishedPanel;
    

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
        }

        // When a scene loads and the scene is not the Main Menu, handle certain elements.
        if (arg0.name != LevelInformation.Levels[0].name)
        {
            StarsAcquired = 0;
            GemsAcquired = 0;

            if (!hpOn)
            {
                hpOn = !hpOn;
            }

            OnLevelStart?.Invoke();

            #region WHEN SCENE STARTS, CLEAR LIST OF COLLECTIBLES AND FIND ALL NEW ONES IN CURRENT SCENE LOADED
            // On Scene Loaded, the new level gold has not yet been acquired.
            for(int i = 0; i < StarConditions.Length; i++)
            {
                if (StarConditions[i])
                    StarConditions[i] = false;
            }

            // Clear all gems in array from previous level played (if any)
            if(GemsInCurrentLevel.Count > 0)
            {
                GemsInCurrentLevel.Clear();
            }

            // Gather list of all GEMS in the current loaded scene.
            Collectible[] collectibles = UnityEngine.Object.FindObjectsOfType<Collectible>();

            if(collectibles.Length > 1)
            {
                foreach (Collectible collectible in collectibles)
                {
                    if (collectible.Type == Collectible.CollectibleType.GEM)
                    {
                        if (!GemsInCurrentLevel.Contains(collectible))
                        {
                            //Debug.Log(collectible);
                            GemsInCurrentLevel.Add(collectible);
                        }
                    }
                }
            }

            /*if (collectibles.Length != 0)
            {
                foreach (Collectible collectible in collectibles)
                {
                    if (collectible.Type == Collectible.CollectibleType.GEM)
                    {
                        if (!GemsInCurrentLevel.Contains(collectible))
                        {
                            //Debug.Log(collectible);
                            GemsInCurrentLevel.Add(collectible);
                        }
                    }
                }
            }
            else
            {
                /// If there are no gems in the current level and the list length is 0,
                /// 
            }*/
            #endregion
        }
        else
        {
            // When the game starts, find Audio Sources.
            if (MM_UI.MMUI != null)
            {
                SoundManager.SetAudioSources(MM_UI.MMUI.SFXSoundSource, MM_UI.MMUI.MusicSoundSource);
            }

            if (hpOn)
            {
                hpOn = !hpOn;
            }

        }

        OnToggleHP?.Invoke(hpOn);

        // Play level music accordingly
        HandleAudioSources(SoundManager.MusicSource, SoundManager.SFXSource);
    }

    private static void HandleAudioSources(AudioSource mSource, AudioSource sSource)
    {
        if(LevelIndex == 0)
        {
            if (mSource.isPlaying)
            {
                mSource.Stop();
            }
        }
        else if(LevelIndex > 0 && LevelIndex < 6)
        {
            mSource.clip = SoundManager.LevelMusic1to5;
            mSource.Play();
        }
        else if(LevelIndex > 5 && LevelIndex < 11)
        {
            mSource.clip = SoundManager.LevelMusic6to10;
            mSource.Play();
        }
        else if(LevelIndex > 10 && LevelIndex < 16)
        {
            mSource.clip = SoundManager.LevelMusic11to15;
            mSource.Play();
        }
        else if(LevelIndex > 15 && LevelIndex < 21)
        {
            mSource.clip = SoundManager.LevelMusic16to20;
            mSource.Play();
        }
        else if(LevelIndex > 20 && LevelIndex < 26)
        {
            mSource.clip = SoundManager.LevelMusic21to25;
            mSource.Play();
        }
        else
        {
            mSource.clip = SoundManager.LevelMusic26to30;
            mSource.Play();
        }
    }

    public static void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelIndex != 0)
            {
                isPaused = !isPaused;
                TogglePause(isPaused);

                if (isPaused)
                {
                    if (SoundManager.MusicSource.isPlaying)
                    {
                        SoundManager.MusicSource.Pause();
                    }
                }
                else
                {
                    if (!SoundManager.MusicSource.isPlaying)
                    {
                        SoundManager.MusicSource.Play();
                    }
                }

                
            }
        }
    }

    private static void UI_OnRetry()
    {
        //Debug.Log("This is 'UI_OnRetry' contained in GameManager class.");

        isPaused = !isPaused;
        TogglePause(false);
        Player_OnPlayerIsDead(false);
        OnToggleSuccessPanel?.Invoke(false);
        OnToggleGameFinishedPanel(false);

        SceneManager.LoadScene(LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex].name);
    }

    private static void UI_OnMainMenu()
    {
        //Debug.Log("This is the 'UI_OnMainMenu' method contained in the GameManager class.");

        isPaused = !isPaused;
        TogglePause(false);
        Player_OnPlayerIsDead(false);
        OnToggleSuccessPanel?.Invoke(false);
        OnToggleGameFinishedPanel(false);

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
                if (!StarConditions[0])
                {
                    StarConditions[0] = true;
                    if (SoundManager.MusicSource.isPlaying)
                    {
                        Debug.Log("Gold Collected! Music Should Stop Now!");
                        SoundManager.MusicSource.Stop();
                    }
                }
                break;

            case Collectible.CollectibleType.GEM:
                if (GemsInCurrentLevel.Contains(collectible))
                {
                    GemsInCurrentLevel.Remove(collectible);
                    GemsAcquired += 1;
                }

                //SoundManager.PlaySound(SoundManager.GemPickup);

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
            // Stop the level music

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

            switch (StarsAcquired)
            {
                case 1:
                    SoundManager.PlaySound(SoundManager.Win1);
                    break;

                case 2:
                    SoundManager.PlaySound(SoundManager.Win2);
                    break;

                case 3:
                    SoundManager.PlaySound(SoundManager.Win3);
                    break;
            }

            // assign collected gems to the level
            if(LevelInformation.Levels[LevelIndex].gemsAcquired < GemsAcquired)
            {
                LevelInformation.Levels[LevelIndex].gemsAcquired = GemsAcquired;
            }

            // Assign the awarded stars to the level and unlock the next level.
            //LevelInformation.Levels[LevelIndex].stars = (Level.LevelStars)StarsAcquired;
            if(LevelInformation.Levels[LevelIndex].stars < (Level.LevelStars)StarsAcquired)
            {
                LevelInformation.Levels[LevelIndex].stars = (Level.LevelStars)StarsAcquired;
            }

            if (LevelIndex != LevelInformation.Levels.Length - 1)
            {
                //Debug.Log($"Level Index : {LevelIndex}. Levels Length: {LevelInformation.Levels.Length}.");
                if (LevelInformation.Levels[LevelIndex + 1].levelLocked)
                {
                    LevelInformation.Levels[LevelIndex + 1].levelLocked = false;
                }

                OnToggleSuccessPanel?.Invoke(true);
            }
            else
            {
                OnToggleGameFinishedPanel?.Invoke(true);
            }

            SaveSystem.SaveGame();

            
        }
    }

    private static void Player_OnPlayerIsDead(bool value)
    {
        OnToggleDeathPanel?.Invoke(value);
    }

    private static void TogglePause(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        Player.DisablePlayer(value);

        OnPause?.Invoke(value);
    }
}
