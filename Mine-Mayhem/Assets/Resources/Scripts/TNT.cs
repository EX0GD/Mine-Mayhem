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

    // Start is called before the first frame update
    void Start()
    {
        detonate = false;
        hasExploded = false;
        animator = GetComponentInChildren<Animator>();
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
                animator.SetBool("isTriggered", true);
            }

            // When the boom animation finishes playing, destroy the object.
            // If the TNT has been triggered...
            if(animator.GetBool("isTriggered") == true)
            {
                // if the name of the current animation state is "Explode"...
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Explode"))
                {
                    // and if the animation has reached its length...
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        // destroy the object (after the animation has finished playing.
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
}
