﻿using System;
using System.Collections;
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

    // Animation state strings
    private const string idleAnim = "MinerIdle";
    private const string runAnim = "MinerRun";
    private const string bjAnim = "MinerBJ";
    private const string inAirAnim = "MinerFall";
    private const string wallGrabAnim = "MinerWG";
    private const string deadAnim = "MinerDead";

    private Dictionary<PlayerStates, string> animStates;

    public Rigidbody2D RB { get; private set; }
    private SpriteRenderer PlayerSpriteRenderer { get; set; }
    private CapsuleCollider2D MainCollider { get; set; }
    private Animator PlayerAnimator { get; set; }
    private Animator ExplosionAnimator { get; set; }
    private CapsuleCollider2D ExplosionCollider { get; set; }

    [Range(1,100)]
    public int maxPlayerHealth;
    public int curPlayerHealth;
    public float inputX;
    public float runSpeed;
    public bool canMove = true;
    public bool grounded;
    public bool canJump = true;
    public int explosiveForce;
    public int explosiveDamage;
    public bool onWall = false;
    public float bombCoolDownTimer;
    public float bombCoolDownTime;

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

        animStates = new Dictionary<PlayerStates, string>()
        {
            {PlayerStates.IDLE, idleAnim},
            {PlayerStates.RUN, runAnim},
            {PlayerStates.BOOM_JUMP, bjAnim},
            {PlayerStates.IN_AIR, inAirAnim},
            {PlayerStates.WALL_GRAB, wallGrabAnim},
            {PlayerStates.DEAD, idleAnim}
        };

        RB = GetComponent<Rigidbody2D>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        MainCollider = GetComponent<CapsuleCollider2D>();
        PlayerAnimator = GetComponent<Animator>();
        ExplosionAnimator = transform.GetChild(0).GetComponent<Animator>();
        ExplosionCollider = transform.GetChild(0).GetComponent<CapsuleCollider2D>();

        if (!MainCollider.enabled)
        {
            MainCollider.enabled = true;
        }

        // Set the players starting current health. 
        if(curPlayerHealth != maxPlayerHealth)
        {
            curPlayerHealth = maxPlayerHealth;
        }

        bombCoolDownTimer = bombCoolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        psm[currentState].Invoke();
        PlayAnimation();
        HandleBombJumpCoolDownTimer();
    }

    private void FixedUpdate()
    {
        //psm[currentState].Invoke();
        grounded = IsGrounded();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            if (!onWall)
                onWall = !onWall;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            if (onWall)
                onWall = !onWall;
        }
    }

    private void SetState(PlayerStates state)
    {
        if(currentState != state)
        {
            currentState = state;
        }
    }

    private void PlayAnimation()
    {
        PlayerAnimator.Play(animStates[currentState]);
    }

    private void HandleBombJumpCoolDownTimer()
    {
        if (!canJump && currentState != PlayerStates.DEAD)
        {
            if(bombCoolDownTimer > 0)
            {
                bombCoolDownTimer -= Time.deltaTime;
            }
            else
            {
                ExplosionAnimator.SetBool("isTriggered", false);
                canJump = true;
                bombCoolDownTimer = bombCoolDownTime;
            }
        }
    }

    public void DamagePlayer(int dmg)
    {
        if(curPlayerHealth <= dmg)
        {
            curPlayerHealth -= curPlayerHealth;
            if(curPlayerHealth == 0)
            {
                SetState(PlayerStates.DEAD);
            }
        }
        else
        {
            curPlayerHealth -= dmg;
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
        Collider2D col = Physics2D.OverlapCapsule(new Vector2(MainCollider.transform.position.x, MainCollider.transform.position.y - extraHeight), 
                                                              new Vector2(MainCollider.size.x - 0.2f, MainCollider.size.y), CapsuleDirection2D.Vertical, 0, worldLayer);
        
        return col != null;
    }

    // --------------------------- STATE FUNCTIONS --------------------------------//
    private void HandleIdle()
    {
        //Debug.Log("This is the 'HandleIdle' function.");

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
        //Debug.Log("This is the 'HandleRun' function.");

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
        if (canJump)
        {
            canJump = !canJump;
            //Debug.Log("This is the 'HandleBoomJump' function.");
            ExplosionAnimator.SetBool("isTriggered", true);
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.AddForce(Vector2.up * explosiveForce, ForceMode2D.Impulse);
            // Damage player
            DamagePlayer(explosiveDamage);
        }

        Collider2D[] cols = Physics2D.OverlapCapsuleAll(ExplosionCollider.bounds.center, ExplosionCollider.size, CapsuleDirection2D.Horizontal, 0);
        foreach(Collider2D col in cols)
        {
            if(col.GetComponent<TNT>() != null)
            {
                TNT tnt = col.GetComponent<TNT>();
                tnt.Detonate(true);
            }
        }


        if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (!grounded)
            {
                SetState(PlayerStates.IN_AIR);
            }
        }
    }

    private void HandleInAir()
    {
        //Debug.Log("This is the 'HandleInAir' function.");
        Movement();
        TransitionToJump();

        if (grounded)
        {
            if (inputX != 0)
            {
                SetState(PlayerStates.RUN);
            }
            else
            {
                SetState(PlayerStates.IDLE);
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift) && onWall)
            {
                SetState(PlayerStates.WALL_GRAB);
            }
        }
    }

    private void HandleWallGrab()
    {
        //Debug.Log("This is the 'HandleWallGrab' function.");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Small directional raycasts to detect which side wall
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, worldLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, worldLayer);
            if(hitLeft.collider != null)
            {
                if (!PlayerSpriteRenderer.flipX)
                {
                    PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
                }
            }
            else if(hitRight.collider != null)
            {
                if (PlayerSpriteRenderer.flipX)
                {
                    PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
                }
            }

            if (RB.constraints != RigidbodyConstraints2D.FreezeAll)
            {
                RB.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            if(RB.constraints == RigidbodyConstraints2D.FreezeAll)
            {
                RB.constraints = RigidbodyConstraints2D.None;
                if (!RB.freezeRotation)
                {
                    RB.freezeRotation = !RB.freezeRotation;
                }
                RB.AddForce(Vector2.down);
                SetState(PlayerStates.IN_AIR);
            }
        }
    }

    private void HandleDead()
    {
        Debug.Log("This is the 'HandleDead' function.");
        // Turn everything off
        if (canMove)
            canMove = !canMove;

        if (canJump)
            canJump = !canJump;

        if (MainCollider.enabled)
        {
            MainCollider.enabled = false;
        }
    }
}
