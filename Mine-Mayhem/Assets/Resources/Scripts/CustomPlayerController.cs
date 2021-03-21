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

    private Rigidbody2D RB { get; set; }
    private SpriteRenderer PlayerSpriteRenderer { get; set; }
    private CapsuleCollider2D MainCollider { get; set; }

    [Range(1,100)]
    public int maxPlayerHealth;
    public int curPlayerHealth;
    public float inputX;
    public float runSpeed;
    public bool canMove = true;
    public bool grounded;
    public bool canJump = true;
    public int explosiveForce;

    public LayerMask worldLayer;

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

        // Set players' starting state.
        SetState(PlayerStates.IDLE);
        RB = GetComponent<Rigidbody2D>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        MainCollider = GetComponent<CapsuleCollider2D>();

        // Set the players starting current health. 
        if(curPlayerHealth != maxPlayerHealth)
        {
            curPlayerHealth = maxPlayerHealth;
        }

    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        psm[currentState].Invoke();
        grounded = IsGrounded();
    }

    private void SetState(PlayerStates state)
    {
        if(currentState != state)
        {
            currentState = state;
        }
    }

    private void Movement()
    {
        if (canMove)
        {
            Vector2 movement = new Vector2(inputX * (runSpeed), RB.velocity.y);
            if (RB.velocity != movement)
            {
                RB.velocity = movement;
            }

            // Flip the sprite depending on velocity
            if (inputX > 0)
            {
                if (PlayerSpriteRenderer.flipX != true)
                {
                    PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
                }
            }
            else
            {
                if (PlayerSpriteRenderer.flipX != false)
                {
                    PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
                }
            }
        }
    }

    private void TransitionToJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            SetState(PlayerStates.BOOM_JUMP);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        Collider2D col = Physics2D.OverlapCapsule(new Vector2(MainCollider.transform.position.x, MainCollider.transform.position.y - extraHeight), MainCollider.size, CapsuleDirection2D.Vertical, 0, worldLayer);

        return col != null;
    }

    // --------------------------- STATE FUNCTIONS --------------------------------//
    private void HandleIdle()
    {
        Debug.Log("This is the 'HandleIdle' function.");

        if(inputX != 0)
        {
            SetState(PlayerStates.RUN);
        }
        // if player is not trying to move
        else
        {
            if (grounded)
            {
                if (RB.velocity != Vector2.zero)
                {
                    RB.velocity = Vector2.zero;
                }
                TransitionToJump();
            }
        }
    }

    private void HandleRun()
    {
        Debug.Log("This is the 'HandleRun' function.");

        if(inputX != 0)
        {
            Movement();
        }
        else
        {
            SetState(PlayerStates.IDLE);
        }

        TransitionToJump();
    }

    private void HandleBoomJump()
    {
        Debug.Log("This is the 'HandleBoomJump' function.");
        RB.AddForce(Vector2.up * explosiveForce, ForceMode2D.Impulse);
        // Damage player
        SetState(PlayerStates.IN_AIR);
    }

    private void HandleInAir()
    {
        Debug.Log("This is the 'HandleInAir' function.");
        Movement();

        if (grounded)
        {
            if(inputX != 0)
            {
                SetState(PlayerStates.RUN);
            }
            else
            {
                SetState(PlayerStates.IDLE);
            }
        }
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
