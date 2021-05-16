using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Animator MainMenuAnimator { get; set; }

    [SerializeField] private int htpIndex;

    public GameObject[] htpMenus;

    [Space(10)]
    [SerializeField] private int levelIndex;
    public TextMeshProUGUI levelText;
    public GameObject levelLock;
    public Image[] levelStarIMGs;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuAnimator = GetComponentInChildren<Animator>();
        htpIndex = 0;
        levelIndex = 1;
        EditLevelInfo(LevelInformation.Levels[levelIndex].displayName, LevelInformation.Levels[levelIndex].levelLocked, LevelInformation.Levels[levelIndex].stars);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EditLevelInfo(string levelName, bool levelLocked, Level.LevelStars rating)
    {
        if(levelText.text != levelName)
        {
            levelText.text = levelName;
        }

        if(levelLock.activeSelf != levelLocked)
        {
            levelLock.SetActive(levelLocked);
        }

        switch (rating)
        {
            case Level.LevelStars.ZERO:
                for(int i = 0; i < levelStarIMGs.Length; i++)
                {
                    if (levelStarIMGs[i].enabled)
                    {
                        levelStarIMGs[i].enabled = false;
                    }
                }
                break;

            case Level.LevelStars.Star1:
                for(int i = 0; i < levelStarIMGs.Length; i++)
                {
                    if(i < levelStarIMGs.Length - 2)
                    {
                        if (!levelStarIMGs[i].enabled)
                        {
                            levelStarIMGs[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (levelStarIMGs[i].enabled)
                        {
                            levelStarIMGs[i].enabled = false;
                        }
                    }
                }
                break;

            case Level.LevelStars.Star2:
                for(int i = 0; i < levelStarIMGs.Length; i++)
                {
                    if(i < levelStarIMGs.Length - 1)
                    {
                        if (!levelStarIMGs[i].enabled)
                        {
                            levelStarIMGs[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (levelStarIMGs[i].enabled)
                        {
                            levelStarIMGs[i].enabled = false;
                        }
                    }
                }
                break;

            case Level.LevelStars.Star3:
                for(int i = 0; i < levelStarIMGs.Length; i++)
                {
                    if (!levelStarIMGs[i].enabled)
                    {
                        levelStarIMGs[i].enabled = true;
                    }
                }
                break;
        }
    }


    // ------------------------- BUTTON FUNCTIONS --------------------------//
    public void PlayButton()
    {
        //Debug.Log("Play button.");
        MainMenuAnimator.SetTrigger("toLevelSelect");
        if (levelIndex != 1)
        {
            levelIndex = 1;
        }

        EditLevelInfo(LevelInformation.Levels[levelIndex].displayName, LevelInformation.Levels[levelIndex].levelLocked, LevelInformation.Levels[levelIndex].stars);
    }

    public void StartButton()
    {
        // If the chosen level is NOT LOCKED, then load it.
        if (!LevelInformation.Levels[levelIndex].levelLocked)
        {
            SceneManager.LoadScene(LevelInformation.Levels[levelIndex].name, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log($"The current scene is locked: {LevelInformation.Levels[levelIndex].name}.");
        }
    }

    public void HTPButton()
    {
        //Debug.Log("How to Play Button");
        MainMenuAnimator.SetTrigger("ToHTP");

        if(htpIndex != 0)
        {
            htpIndex = 0;
        }
        for (int i = 0; i < htpMenus.Length; i++)
        {
            if (i == htpIndex)
            {
                if (!htpMenus[i].activeSelf)
                {
                    htpMenus[i].SetActive(true);
                }
            }
            else
            {
                if (htpMenus[i].activeSelf)
                {
                    htpMenus[i].SetActive(false);
                }
            }
        }
    }

    public void CreditsButton()
    {
        //Debug.Log("Credits Button");
        MainMenuAnimator.SetTrigger("ToCredits");
    }

    public void BackToMain()
    {
        //Debug.Log("Back to Main Menu.");

        if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("LevelSelect"))
        {
            MainMenuAnimator.SetTrigger("LeaveLevelSelect");
        }
        else if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("HowtoPlay"))
        {
            MainMenuAnimator.SetTrigger("LeaveHTP");
        }
        else if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Credits"))
        {
            MainMenuAnimator.SetTrigger("LeaveCredits");
        }
    }

    public void LeftArrowButton()
    {
        //Debug.Log("This is the left arrow button.");

        if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("HowtoPlay"))
        {
            htpIndex--;

            if(htpIndex < 0)
            {
                htpIndex = htpMenus.Length - 1;
            }

            for(int i = 0; i < htpMenus.Length; i++)
            {
                if(i == htpIndex)
                {
                    if (!htpMenus[i].activeSelf)
                    {
                        htpMenus[i].SetActive(true);
                    }
                }
                else
                {
                    if (htpMenus[i].activeSelf)
                    {
                        htpMenus[i].SetActive(false);
                    }
                }
            }
        }
        else if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("LevelSelect"))
        {
            levelIndex--;

            if(levelIndex < 1)
            {
                levelIndex = SceneManager.sceneCountInBuildSettings - 1;
            }

            EditLevelInfo(LevelInformation.Levels[levelIndex].displayName, LevelInformation.Levels[levelIndex].levelLocked, LevelInformation.Levels[levelIndex].stars);
        }
    }

    public void RightArrowButton()
    {
        //Debug.Log("This is the right arrow button.");

        if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("HowtoPlay"))
        {
            htpIndex++;

            if (htpIndex > htpMenus.Length - 1)
            {
                htpIndex = 0;
            }

            for (int i = 0; i < htpMenus.Length; i++)
            {
                if (i == htpIndex)
                {
                    if (!htpMenus[i].activeSelf)
                    {
                        htpMenus[i].SetActive(true);
                    }
                }
                else
                {
                    if (htpMenus[i].activeSelf)
                    {
                        htpMenus[i].SetActive(false);
                    }
                }
            }
        }
        else if (MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("LevelSelect"))
        {
            levelIndex++;

            if (levelIndex > SceneManager.sceneCountInBuildSettings - 1)
            {
                levelIndex = 1;
            }

            EditLevelInfo(LevelInformation.Levels[levelIndex].displayName, LevelInformation.Levels[levelIndex].levelLocked, LevelInformation.Levels[levelIndex].stars);
        }
    }

    public void QuitApplication()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }
}
