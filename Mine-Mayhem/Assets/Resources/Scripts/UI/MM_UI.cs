using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MM_UI : MonoBehaviour
{
    public HPBar hp;
    public FailedPanelSet failPanel;
    public PausedPanelSet pausePanel;
    public SuccessPanelSet successPanel;
    public IntroQuip introQuip;

    public bool hpActive;
    public bool failPanelActive;
    public bool successPanelActive;
    public bool IntroQuipActive;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>() != null ? gm = FindObjectOfType<GameManager>() : gm = null;
        Debug.Log(gm);

        gm.OnPause += PauseEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleHP(bool value)
    {
        if (hp.gameObject.activeSelf != value)
        {
            hp.gameObject.SetActive(value);
        }
    }

    public void ToggleFailPanel(bool value)
    {
        if(failPanel.gameObject.activeSelf != value)
        {
            failPanel.gameObject.SetActive(value);
        }
    }

    public void TogglePause(bool value)
    {
        if(pausePanel.gameObject.activeSelf != value)
        {
            pausePanel.gameObject.SetActive(value);
        }
    }

    private void PauseEvent(bool value)
    {
        Debug.Log("Currently invoking the 'PauseEvent' function contained in: " + this);
        hpActive = value;
        if (pausePanel.gameObject.activeSelf != hpActive) 
        {
            pausePanel.gameObject.SetActive(hpActive);
        }
    }
    // ------------------- Button Functions ------------------------//
    public void RetryButton()
    {
        Debug.Log("Retry Button");
        if(SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByBuildIndex(0).buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // 
        }
    }

    public void MainMenuButton()
    {
        Debug.Log("MainMenuButton");
    }

    public void SQButton()
    {
        Debug.Log("Save / Quit Button");
    }

    public void SoundToggleButton()
    {
        Debug.Log("Sound Toggle Button");
    }

    public void NextLevelButton()
    {
        Debug.Log("Next Level Button");
    }
}
