using System;
using UnityEngine;

public class MM_UI : MonoBehaviour
{
    public HPBar hp;
    public FailedPanelSet failPanel;
    public PausedPanelSet pausePanel;
    public SuccessPanelSet successPanel;
    public IntroQuip introQuip;

    [SerializeField] private bool hpActive;
    [SerializeField] public bool failPanelActive;
    [SerializeField] public bool pausePanelActive;
    [SerializeField] public bool successPanelActive;
    [SerializeField] public bool IntroQuipActive;

    public static event Action OnRetry;
    public static event Action OnMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.OnPause += TogglePauseEvent;
        GameManager.OnToggleHP += ToggleHPEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameManager.OnPause -= TogglePauseEvent;
        GameManager.OnToggleHP -= ToggleHPEvent;
    }

    private void ToggleHPEvent(bool value)
    {
        Debug.Log("'Toggle HP Event' just triggered.");

        if (hpActive != value)
            hpActive = value;

        if (hp.gameObject.activeSelf != hpActive)
            hp.gameObject.SetActive(hpActive);
    }

    public void ToggleFailPanelEvent(bool value)
    {
        Debug.Log("'Toggle Fail Panel Event' just triggered.");

        if (failPanelActive != value)
            failPanelActive = value;

        if (failPanel.gameObject.activeSelf != failPanelActive)
            failPanel.gameObject.SetActive(failPanelActive);
    }

    private void TogglePauseEvent(bool value)
    {
        Debug.Log("Currently invoking the 'PauseEvent' function contained in: " + this);

        if (pausePanelActive != value)
            pausePanelActive = value;

        if (pausePanel.gameObject.activeSelf != pausePanelActive)
            pausePanel.gameObject.SetActive(pausePanelActive);
    }
    // ------------------- Button Functions ------------------------//
    public void RetryButton()
    {
        Debug.Log("Retry Button");
        OnRetry?.Invoke();
    }

    public void MainMenuButton()
    {
        Debug.Log("MainMenuButton");
        OnMainMenu?.Invoke();
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
