using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator MainMenuAnimator { get; set; }

    [SerializeField] private int htpIndex;

    public GameObject[] htpMenus;

    [Space(10)]
    [SerializeField] private int levelIndex;
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuAnimator = GetComponentInChildren<Animator>();
        htpIndex = 0;
        levelIndex = 1;
        EditLevelText(GameManager.Mm_Scenes[levelIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EditLevelText(string text)
    {
        if(levelText.text != text)
        {
            levelText.text = text;
        }
    }

    public void PlayButton()
    {
        //Debug.Log("Play button.");
        MainMenuAnimator.SetTrigger("toLevelSelect");
        if (levelIndex != 1)
        {
            levelIndex = 1;
        }
        EditLevelText(GameManager.Mm_Scenes[levelIndex]);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(GameManager.Mm_Scenes[levelIndex], LoadSceneMode.Single);
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

            EditLevelText(GameManager.Mm_Scenes[levelIndex]);
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

            EditLevelText(GameManager.Mm_Scenes[levelIndex]);
        }
    }

    public void QuitApplication()
    {
        Debug.Log("This is the 'Quit Application' button.");
    }
}
