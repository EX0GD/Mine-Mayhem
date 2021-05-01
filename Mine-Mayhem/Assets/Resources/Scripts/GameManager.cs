using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public static class GameManager
{
    //public static GameManager GM { get; private set; }
    public static MM_UI MMUI { get; private set; }

    //public static CustomPlayerController Player { get; private set; }
    public static CustomPlayerController Player
    {
        get
        {
            CustomPlayerController player = UnityEngine.Object.FindObjectOfType<CustomPlayerController>();
            //Debug.Log(player);
            if(player != null)
            {
                return player;
            }
            else
            {
                return null;
            }
        }
    }

    private const string mm_MainMenuScene = "MainMenu";
    private const string mm_Scene1 = "MM_Level1";
    private const string mm_Scene2 = "MM_Level2";
    private const string mm_Scene3 = "MM_Level3";
    private const string mm_Scene4 = "MM_Level4";
    private const string mm_Scene5 = "MM_Level5";
    private const string mm_Scene6 = "MM_Level6";
    private const string mm_Scene7 = "MM_Level7";

    public static string[] Mm_Scenes { get; private set; }
    public static int LevelIndex { get; private set; }

    public static int GoldCollected;
    public static int GemsCollected;

    private static bool isPaused = false;
    private static bool hpOn = false;

    public static event Action<bool> OnPause;
    public static event Action<bool> OnToggleHP;
    public static event Action OnLevelStart;

    static GameManager()
    {
        Mm_Scenes = new string[SceneManager.sceneCountInBuildSettings];
        Mm_Scenes[0] = mm_MainMenuScene;
        Mm_Scenes[1] = mm_Scene1;
        Mm_Scenes[2] = mm_Scene2;
        Mm_Scenes[3] = mm_Scene3;
        Mm_Scenes[4] = mm_Scene4;
        Mm_Scenes[5] = mm_Scene5;
        Mm_Scenes[6] = mm_Scene6;
        Mm_Scenes[7] = mm_Scene7;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        MM_UI.OnRetry += UI_OnRetry;
        MM_UI.OnMainMenu += UI_OnMainMenu;
        MM_UI.OnQuit += UI_OnQuit;
        Collectible.OnPickUpCollectible += Collectible_OnPickUp;
    }

    private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Debug.Log($"{arg0.name} just finished loading. Load scene mode = {arg1}.");

        // When a scene loads, store the levels build index in 'LevelIndex'.
        if (LevelIndex != arg0.buildIndex)
        {
            LevelIndex = arg0.buildIndex;
            //Debug.Log($"Level Index: {LevelIndex}");
        }

        // When a scene loads and the scene is not the Main Menu, handle certain elements.
        if (arg0.name != Mm_Scenes[0])
        {
            if (!hpOn)
            {
                hpOn = !hpOn;
            }

            Debug.Log("HP bar is now turned on.");
            OnLevelStart?.Invoke();
        }
        else
        {
            if (hpOn)
            {
                hpOn = !hpOn;
            }
            Debug.Log("HP bar is now turned off.");
        }

        OnToggleHP?.Invoke(hpOn);
    }

    public static void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByBuildIndex(0).buildIndex)
            {
                Debug.Log("Just pressed the 'PAUSE' button.");
                isPaused = !isPaused;
                TogglePause(isPaused);
            }
            else
            {
                Debug.Log("You cannot pause in the current scene.");
            }
        }
    }

    private static void UI_OnRetry()
    {
        Debug.Log("This is 'UI_OnRetry' contained in GameManager class.");

        TogglePause(false);

        SceneManager.LoadScene(Mm_Scenes[SceneManager.GetActiveScene().buildIndex]);
    }

    private static void UI_OnMainMenu()
    {
        Debug.Log("This is the 'UI_OnMainMenu' method contained in the GameManager class.");

        TogglePause(false);

        SceneManager.LoadScene(Mm_Scenes[0]);
    }

    private static void UI_OnQuit()
    {
        Debug.Log("This is the 'UI_OnQuit' method contained in the GameManager class.");
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
    }

    private static void Collectible_OnPickUp()
    {
        //Debug.Log("This is the pick up function on GameManager.");
        GoldCollected++;
        Debug.Log(GoldCollected);
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
