using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_UI : MonoBehaviour
{
    public HPBar hp;
    public FailedPanelSet failPanel;
    public PausedPanelSet pausePanel;
    public SuccessPanelSet successPanel;
    public IntroQuip introQuip;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetryButton()
    {
        Debug.Log("Retry Button");
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
