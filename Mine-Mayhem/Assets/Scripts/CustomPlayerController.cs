using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        RUN,
        BOOM_JUMP,
        IN_AIR,
        WALL_GRAB,
        DEAD
    }
    public PlayerStates currentState;

    private Dictionary<PlayerStates, Action> psm;

    // Start is called before the first frame update
    void Start()
    {
        psm = new Dictionary<PlayerStates, Action>
        {
            {PlayerStates.IDLE, HandleIdle},
            {PlayerStates.RUN, HandleRun},
            {PlayerStates.BOOM_JUMP, HandleBoomJump},
            {PlayerStates.IN_AIR, HandleInAir},
            {PlayerStates.WALL_GRAB, HandleWallGrab},
            {PlayerStates.DEAD, HandleDead}
        };

        SetState(PlayerStates.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        psm[currentState].Invoke();
    }

    private void SetState(PlayerStates state)
    {
        if(currentState != state)
        {
            currentState = state;
        }
    }

    private void HandleIdle()
    {
        Debug.Log("This is the 'HandleIdle' function.");
    }

    private void HandleRun()
    {
        Debug.Log("This is the 'HandleRun' function.");
    }

    private void HandleBoomJump()
    {
        Debug.Log("This is the 'HandleBoomJump' function.");
    }

    private void HandleInAir()
    {
        Debug.Log("This is the 'HandleInAir' function.");
    }

    private void HandleWallGrab()
    {
        Debug.Log("This is the 'HandleWallGrab' function.");
    }

    private void HandleDead()
    {
        Debug.Log("This is the 'HandleDead' function.");
    }
}
