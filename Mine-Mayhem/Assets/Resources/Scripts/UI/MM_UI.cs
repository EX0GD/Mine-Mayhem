using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MM_UI : MonoBehaviour
{
    public static MM_UI MMUI;
    [SerializeField] private AudioSource musicSoundSource = null;
    public AudioSource MusicSoundSource { get { return musicSoundSource; } }

    [SerializeField] private AudioSource sfxSoundSource = null;
    public AudioSource SFXSoundSource { get { return sfxSoundSource; } }

    public HPBar hp;
    public FailedPanelSet failPanel;
    public PausedPanelSet pausePanel;
    public SuccessPanelSet successPanel;
    public IntroQuip introQuip;
    public GameFinishedPanelSet gameFinishedPanel;
    public Image[] rewardStarImages;
    public Image[] finalRewardStarImages;

    [SerializeField] private bool hpActive = false;
    [SerializeField] private bool failPanelActive = false;
    [SerializeField] private bool pausePanelActive = false;
    [SerializeField] private bool successPanelActive = false;
    [SerializeField] private bool IntroQuipActive = false;
    [SerializeField] private bool gameFinishedPanelActive = false;

    [SerializeField] private TextMeshProUGUI introQuipLevelText = null;

    [SerializeField] private GameObject levelgems = null;
    [SerializeField] private Image[] lvlGemImgs = null;
    [SerializeField] private GameObject finalLevelGems = null;
    [SerializeField] private Image[] finalLvlGemImgs = null;

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
        GameManager.OnToggleGameFinishedPanel += ToggleGameFinishedPanelEvent;
    }

    // Update is called once per frame
    void Update()
    {
        HandleIntroQuip();
    }

    private void OnDisable()
    {
        GameManager.OnPause -= TogglePauseEvent;
        GameManager.OnToggleHP -= ToggleHPEvent;
        GameManager.OnLevelStart -= LevelStartEvent;
        CustomPlayerController.OnPlayerTakeDamage -= Player_OnTakeDamage;
        GameManager.OnToggleDeathPanel -= ToggleFailPanelEvent;
        GameManager.OnToggleSuccessPanel -= ToggleSuccessPanelEvent;
        GameManager.OnToggleGameFinishedPanel -= ToggleGameFinishedPanelEvent;
    }

    private void HandleIntroQuip()
    {
        if (IntroQuipActive)
        {
            if (Input.anyKeyDown)
            {
                IntroQuipActive = false;
                ToggleIntroQuip(IntroQuipActive);
            }
        }
    }

    private void ToggleIntroQuip(bool value)
    {
        if(IntroQuipActive != value)
            IntroQuipActive = value;

        if (introQuip.gameObject.activeSelf != IntroQuipActive)
            introQuip.gameObject.SetActive(IntroQuipActive);
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
            AdjustStars(GameManager.StarsAcquired, rewardStarImages);
            AdjustGems(LevelInformation.Levels[GameManager.LevelIndex].gems, levelgems, lvlGemImgs);
        }
    }

    private void Player_OnTakeDamage()
    {
        hp.lifeBar.fillAmount = GameManager.Player.curPlayerHealth / GameManager.Player.maxPlayerHealth;
    }

    private void LevelStartEvent()
    {
        //Debug.Log("This is the LevelStartEvent.");
        introQuipLevelText.text = LevelInformation.Levels[GameManager.LevelIndex].displayName;
        ToggleIntroQuip(true);
        hp.lifeBar.fillAmount = GameManager.Player.curPlayerHealth / GameManager.Player.maxPlayerHealth;
    }

    private void ToggleGameFinishedPanelEvent(bool value)
    {
        if(gameFinishedPanelActive != value)
        {
            gameFinishedPanelActive = value;
        }

        if(gameFinishedPanel.gameObject.activeSelf != gameFinishedPanelActive)
        {
            gameFinishedPanel.gameObject.SetActive(gameFinishedPanelActive);
        }

        if (gameFinishedPanelActive)
        {
            AdjustStars(GameManager.StarsAcquired, finalRewardStarImages);
            AdjustGems(LevelInformation.Levels[GameManager.LevelIndex].gems, finalLevelGems, finalLvlGemImgs);
        }
    }

    // ------------------- Helper Functions ------------------------//
    private void AdjustStars(int gmStars, Image[] starImages)
    {
        switch (gmStars)
        {
            case 1:
                Debug.Log("Got 1 star.");
                for (int i = 0; i < starImages.Length; i++)
                {
                    if (i < starImages.Length - 2)
                    {
                        if (!starImages[i].enabled)
                        {
                            starImages[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (starImages[i].enabled)
                        {
                            starImages[i].enabled = false;
                        }
                    }
                }
                break;

            case 2:
                Debug.Log("Got 2 stars.");
                for (int i = 0; i < starImages.Length; i++)
                {
                    if (i < starImages.Length - 1)
                    {
                        if (!starImages[i].enabled)
                        {
                            starImages[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (starImages[i].enabled)
                        {
                            starImages[i].enabled = false;
                        }
                    }
                }
                break;

            case 3:
                Debug.Log("Got 3 stars.");
                for (int i = 0; i < starImages.Length; i++)
                {
                    if (!starImages[i].enabled)
                    {
                        starImages[i].enabled = true;
                    }
                }
                break;
        }
    }
    
    private void AdjustGems(Level.LevelGems gems, GameObject lvlGems, Image[] gemImages)
    {
        switch (gems)
        {
            case Level.LevelGems.NONE:
                if (lvlGems.activeSelf)
                {
                    lvlGems.SetActive(false);
                }
                break;

            case Level.LevelGems.Gem1:
                if (!lvlGems.activeSelf)
                {
                    lvlGems.SetActive(true);
                }

                for (int i = 0; i < gemImages.Length; i++)
                {
                    if (i == 0)
                    {
                        if (!gemImages[i].gameObject.activeSelf)
                        {
                            gemImages[i].gameObject.SetActive(true);
                        }

                        gemImages[i].enabled = GameManager.GemsAcquired > i;
                    }
                    else
                    {
                        if (gemImages[i].gameObject.activeSelf)
                        {
                            gemImages[i].gameObject.SetActive(false);
                        }
                    }
                }
                break;

            case Level.LevelGems.Gem2:
                if (!lvlGems.activeSelf)
                {
                    lvlGems.SetActive(true);
                }

                for (int i = 0; i < gemImages.Length; i++)
                {
                    if (i < gemImages.Length - 1)
                    {
                        if (!gemImages[i].gameObject.activeSelf)
                        {
                            gemImages[i].gameObject.SetActive(true);
                        }

                        gemImages[i].enabled = GameManager.GemsAcquired > i;
                    }
                    else
                    {
                        if (gemImages[i].gameObject.activeSelf)
                        {
                            gemImages[i].gameObject.SetActive(false);
                        }
                    }
                }
                break;

            case Level.LevelGems.Gem3:
                if (!lvlGems.activeSelf)
                {
                    lvlGems.SetActive(true);
                }

                for (int i = 0; i < gemImages.Length; i++)
                {
                    if (!gemImages[i].gameObject.activeSelf)
                    {
                        gemImages[i].gameObject.SetActive(true);
                    }

                    gemImages[i].enabled = GameManager.GemsAcquired > i;
                }
                break;
        }
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
        //Debug.Log("Next Level Button");
        ToggleSuccessPanelEvent(false);
        SceneManager.LoadScene(LevelInformation.Levels[SceneManager.GetActiveScene().buildIndex + 1].name);
    }
}
