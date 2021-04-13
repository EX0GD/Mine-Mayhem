using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator MainMenuAnimator { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        MainMenuAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        Debug.Log("Play button.");
        MainMenuAnimator.SetTrigger("toLevelSelect");
    }

    public void HTPButton()
    {
        Debug.Log("How to Play Button");
        MainMenuAnimator.SetTrigger("ToHTP");
    }

    public void CreditsButton()
    {
        Debug.Log("Credits Button");
        MainMenuAnimator.SetTrigger("ToCredits");
    }

    public void BackToMain()
    {
        Debug.Log("Back to Main Menu.");

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
        Debug.Log("This is the left arrow button.");
    }

    public void RightArrowButton()
    {
        Debug.Log("This is the right arrow button.");
    }
}
