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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Changing scenes...");
            SceneManager.LoadScene(mm_Scenes[1], LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Changing scenes...");
            SceneManager.LoadScene(mm_Scenes[2], LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for(int i = 0; i < mm_Scenes.Length; i++)
            {
                Debug.Log(mm_Scenes[i]);
            }
        }
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
}
