using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MM_UI : MonoBehaviour
{
    private static MM_UI MMUI;

    public HPBar hp;
    public FailedPanelSet failPanel;
    public PausedPanelSet pausePanel;
    public SuccessPanelSet successPanel;
    public IntroQuip introQuip;
    public Image[] rewardStarImages;

    [SerializeField] private bool hpActive;
    [SerializeField] public bool failPanelActive;
    [SerializeField] public bool pausePanelActive;
    [SerializeField] public bool successPanelActive;
    [SerializeField] public bool IntroQuipActive;

    public static event Action OnRetry;
    public static event Action OnMainMenu;
    public static event Action OnQuit;

    // Start is called before the first frame update
    void Awake()
    {
        if(MMUI != null)
        {
            if(MMUI != this)
            {
                Destroy(MMUI.gameObject);
                MMUI = this;
            }
        }
        else
        {
            MMUI = this;
        }
        DontDestroyOnLoad(MMUI);
        GameManager.OnPause += TogglePauseEvent;
        GameManager.OnToggleHP += ToggleHPEvent;
        GameManager.OnLevelStart += LevelStartEvent;
        CustomPlayerController.OnPlayerTakeDamage += Player_OnTakeDamage;
        GameManager.OnToggleDeathPanel += ToggleFailPanelEvent;
        GameManager.OnToggleSuccessPanel += ToggleSuccessPanelEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameManager.OnPause -= TogglePauseEvent;
        GameManager.OnToggleHP -= ToggleHPEvent;
        GameManager.OnLevelStart -= LevelStartEvent;
        CustomPlayerController.OnPlayerTakeDamage -= Player_OnTakeDamage;
        GameManager.OnToggleDeathPanel -= ToggleFailPanelEvent;
        GameManager.OnToggleSuccessPanel -= ToggleSuccessPanelEvent;
    }

    private void ToggleHPEvent(bool value)
    {
        if (hpActive != value)
            hpActive = value;

        if (hp.gameObject.activeSelf != hpActive)
            hp.gameObject.SetActive(hpActive);
    }

    public void ToggleFailPanelEvent(bool value)
    {
        if (failPanelActive != value)
            failPanelActive = value;

        if (failPanel.gameObject.activeSelf != failPanelActive)
            failPanel.gameObject.SetActive(failPanelActive);
    }

    private void TogglePauseEvent(bool value)
    {
        if (pausePanelActive != value)
            pausePanelActive = value;

        if (pausePanel.gameObject.activeSelf != pausePanelActive)
            pausePanel.gameObject.SetActive(pausePanelActive);
    }

    private void ToggleSuccessPanelEvent(bool value)
    {
        if (successPanelActive != value)
            successPanelActive = value;

        if (successPanel.gameObject.activeSelf != successPanelActive)
            successPanel.gameObject.SetActive(successPanelActive);

        if (successPanelActive)
        {
            Debug.Log(LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex].stars);
            switch (LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex].stars)
            {
                case Level.LevelStars.ZERO:
                    for (int i = 0; i < rewardStarImages.Length; i++)
                    {
                        if (rewardStarImages[i].enabled)
                        {
                            rewardStarImages[i].enabled = false;
                        }
                    }
                    break;

                case Level.LevelStars.Star1:
                    for (int i = 0; i < rewardStarImages.Length; i++)
                    {
                        if (i < rewardStarImages.Length - 2)
                        {
                            if (!rewardStarImages[i].enabled)
                            {
                                rewardStarImages[i].enabled = true;
                            }
                        }
                        else
                        {
                            if (rewardStarImages[i].enabled)
                            {
                                rewardStarImages[i].enabled = false;
                            }
                        }
                    }
                    break;

                case Level.LevelStars.Star2:
                    for (int i = 0; i < rewardStarImages.Length; i++)
                    {
                        if (i < rewardStarImages.Length - 1)
                        {
                            if (!rewardStarImages[i].enabled)
                            {
                                rewardStarImages[i].enabled = true;
                            }
                        }
                        else
                        {
                            if (rewardStarImages[i].enabled)
                            {
                                rewardStarImages[i].enabled = false;
                            }
                        }
                    }
                    break;

                case Level.LevelStars.Star3:
                    for (int i = 0; i < rewardStarImages.Length; i++)
                    {
                        if (!rewardStarImages[i].enabled)
                        {
                            rewardStarImages[i].enabled = true;
                        }
                    }
                    break;
            }
        }
    }

    private void Player_OnTakeDamage()
    {
        hp.lifeBar.fillAmount = GameManager.Player.curPlayerHealth / GameManager.Player.maxPlayerHealth;
    }

    private void LevelStartEvent()
    {
        Debug.Log("This is the LevelStartEvent.");
        hp.lifeBar.fillAmount = GameManager.Player.curPlayerHealth / GameManager.Player.maxPlayerHealth;
    }
    // ------------------- Button Functions ------------------------//
    public void RetryButton()
    {
        OnRetry?.Invoke();
    }

    public void MainMenuButton()
    {
        OnMainMenu?.Invoke();
    }

    public void SQButton()
    {
        OnQuit?.Invoke();
    }

    public void SoundToggleButton()
    {
        Debug.Log("Sound Toggle Button");
    }

    public void NextLevelButton()
    {
        Debug.Log("Next Level Button");
        ToggleSuccessPanelEvent(false);
        SceneManager.LoadScene(LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex + 1].name);
    }
}
