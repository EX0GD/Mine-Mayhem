using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; }
    public static MM_UI MMUI { get; private set; }

    public static CustomPlayerController Player { get; private set; }

    private const string mm_MainMenuScene = "MainMenu";
    private const string mm_Scene1 = "MM_Level1";
    private const string mm_Scene2 = "MM_Level2";
    private const string mm_Scene3 = "MM_Level3";
    private const string mm_Scene4 = "MM_Level4";
    private const string mm_Scene5 = "MM_Level5";
    private const string mm_Scene6 = "MM_Level6";
    private const string mm_Scene7 = "MM_Level7";

    public string[] mm_Scenes { get; private set; }

    public bool isPaused = false;

    public static event Action<bool> OnPause;

    // Start is called before the first frame update
    void Awake()
    {
        SetGM();
        SetUI();

        mm_Scenes = new string[SceneManager.sceneCountInBuildSettings];
        mm_Scenes[0] = mm_MainMenuScene;
        mm_Scenes[1] = mm_Scene1;
        mm_Scenes[2] = mm_Scene2;
        mm_Scenes[3] = mm_Scene3;
        mm_Scenes[4] = mm_Scene4;
        mm_Scenes[5] = mm_Scene5;
        mm_Scenes[6] = mm_Scene6;
        mm_Scenes[7] = mm_Scene7;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        MM_UI.OnRetry += UI_OnRetry;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log($"{arg0.name} just finished loading. Load scene mode = {arg1}.");

        if (arg0.name != mm_Scenes[0])
        {
            MMUI.ToggleHP(true);
            Debug.Log("HP bar is now turned on.");
        }
        else
        {
            MMUI.ToggleHP(false);
            Debug.Log("HP bar is now turned off.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayer();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(GM);
            Debug.Log(MMUI);
            Debug.Log(Player);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for(int i = 0; i < mm_Scenes.Length; i++)
            {
                Debug.Log(mm_Scenes[i]);
            }
        }

        // Handle Pausing
        HandlePause();
    }

    private void SetGM()
    {
        Debug.Log("Setting up the GameManager.");
        if(GM != null)
        {
            if(GM != this)
            {
                Destroy(GM);
                GM = this;
            }
        }
        else
        {
            GM = this;
        }
        DontDestroyOnLoad(GM);
    }

    private void SetUI()
    {
        if(FindObjectOfType<MM_UI>() != null)
        {
            MM_UI ui = FindObjectOfType<MM_UI>();
            if(MMUI != null)
            {
                if(MMUI != ui)
                {
                    Debug.Log("Destroying old UI");
                    Destroy(MMUI);
                    Debug.Log("Old one destroyed, setting new one.");
                    MMUI = ui;
                }
            }
            else
            {
                //Debug.Log("UI was not set, so currently setting it.");
                MMUI = ui;
            }

            DontDestroyOnLoad(MMUI);
        }
    }

    private void SetPlayer()
    {
        // if the currently active scene is NOT the 'Main Menu', THEN get new player referrence.
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (FindObjectOfType<CustomPlayerController>() != null)
            {
                CustomPlayerController p = FindObjectOfType<CustomPlayerController>();
                if (Player != null)
                {
                    if (Player != p)
                    {
                        Debug.Log("Destroying the IMPOSTERRRRR!");
                        Destroy(Player.gameObject);
                        Debug.Log("And replacing him with the real one! :)");
                        Player = p;
                    }
                }
                else
                {
                    Debug.Log("Initially setting the real one (potential future imposter.....).");
                    Player = p;
                }

                DontDestroyOnLoad(Player);
            }
        }
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByBuildIndex(0).buildIndex)
            {
                Debug.Log("Just pressed the 'PAUSE' button.");
                isPaused = !isPaused;
                //MMUI.TogglePause(isPaused);
                Time.timeScale = isPaused ? 0 : 1;
                Player.enabled = !isPaused;

                OnPause?.Invoke(isPaused);
            }
            else
            {
                Debug.Log("You cannot pause in the current scene.");
            }
        }
    }

    private void UI_OnRetry()
    {
        Debug.Log("This is 'UI_OnRetry' contained in GameManager class.");
    }
}
