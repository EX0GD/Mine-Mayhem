using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetryButton()
    {
        Debug.Log("You have pressed the 'Reset' button.");
    }

    public void MainMenuButton()
    {
        Debug.Log("You have pressed the 'Main Menu' button.");
    }

    public void SQButton()
    {
        Debug.Log("You have pressed the 'Save / Quit' button.");
    }

    public void VolumeButton()
    {
        Debug.Log("You have pressed the 'Volume' button.");
    }
}
