using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public bool detonate;
    public bool hasExploded;
    public Animator animator;
    public float explosionRadius;
    public float explosionForce;

    private SpriteRenderer tntSpriteRenderer;
    private CircleCollider2D explosionCollider;

    // Start is called before the first frame update
    void Start()
    {
        detonate = false;
        hasExploded = false;
        tntSpriteRenderer = GetComponent<SpriteRenderer>();
        explosionCollider = GetComponentInChildren<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDetonate();
    }

    private void HandleDetonate()
    {
        if (detonate)
        {
            if (!hasExploded)
            {
                hasExploded = !hasExploded;
                // set 'isTriggered' to TRUE...
                animator.SetBool("IsTriggered", true);
            }

            // When the boom animation finishes playing, destroy the object.
            // If the TNT has been triggered...
            if(animator.GetBool("IsTriggered") == true)
            {
                // Turn off the sprite renderer
                if (tntSpriteRenderer.enabled)
                {
                    tntSpriteRenderer.enabled = false;
                }
                // if the name of the current animation state is "TNT_Boom"...
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("TNT_Boom"))
                {
                    // and if the animation has reached its length, destroy the object (so boom animation finishes first).
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void Detonate(bool value)
    {
        if(detonate != value)
        {
            detonate = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player is within the trigger volume, apply force and damage
        if(collision.GetComponent<CustomPlayerController>() != null)
        {
            CustomPlayerController player = collision.GetComponent<CustomPlayerController>();
            if(player.RB != null)
            {
                //player.RB.AddForce(new Vector2(player.transform.position - transform.position).normalized)
            }
        }
    }
}
