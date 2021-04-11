using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; }
    public static MM_UI MMUI { get; private set; }

    public static CustomPlayerController Player { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        SetGM();
        SetUI();
        SetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //SetPlayer();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(GM);
            Debug.Log(MMUI);
            Debug.Log(Player);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Changing scenes...");
            SceneManager.LoadScene("MM_Level1", LoadSceneMode.Single);
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
                Debug.Log("UI was not set, so currently setting it.");
                MMUI = ui;
            }
        }
        DontDestroyOnLoad(MMUI);
    }

    private void SetPlayer()
    {
        if (FindObjectOfType<CustomPlayerController>() != null)
        {
            CustomPlayerController p = FindObjectOfType<CustomPlayerController>();
            if (Player != null)
            {
                if (Player != p)
                {
                    Debug.Log("Destroying the IMPOSTERRRRR!");
                    Destroy(Player);
                    Debug.Log("And replacing him with the real one! :)");
                    Player = p;
                }
            }
            else
            {
                Debug.Log("Initially setting the real one (potential future imposter.....).");
                Player = p;
            }
        }
        DontDestroyOnLoad(Player);
    }
}
