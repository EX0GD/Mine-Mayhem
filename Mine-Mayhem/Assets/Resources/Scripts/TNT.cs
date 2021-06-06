using UnityEngine;

public class TNT : MonoBehaviour
{
    public bool detonate;
    public bool hasExploded;
    public Animator animator;
    public float explosionRadius;
    public float explosionForce;
    public float damage;
    public float timer;
    public float timeLimit;
    public bool startTimer;

    private AudioSource TNTAudioSource { get { return GetComponent<AudioSource>(); } }

    private SpriteRenderer tntSpriteRenderer;
    private BoxCollider2D tntBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        detonate = false;
        hasExploded = false;
        tntSpriteRenderer = GetComponent<SpriteRenderer>();
        tntBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
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
                //SoundManager.PlaySound(SoundManager.TNT_Explosion, TNTAudioSource);
                if (!TNTAudioSource.isPlaying)
                {
                    TNTAudioSource.Play();
                }
                // Turn off the sprite renderer
                if (tntSpriteRenderer.enabled)
                {
                    tntSpriteRenderer.enabled = false;
                    tntBoxCollider.enabled = false;
                }
                // if the name of the current animation state is "TNT_Boom"...
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("TNT_Boom"))
                {
                    // and if the animation has reached its length, destroy the object (so boom animation finishes first).
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && TNTAudioSource.time >= 2)
                    {
                        Debug.Log($"Current Playback Time: {TNTAudioSource.time}, Current Clip Length: {TNTAudioSource.clip.length}.");
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
                Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;

                player.RB.AddForce(dir * explosionForce, ForceMode2D.Impulse);
                player.DamagePlayer(damage);
            }
        }

        if(collision.GetComponent<TNT>() != null)
        {
            TNT tnt = collision.GetComponent<TNT>();

            if(tnt != null && !tnt.hasExploded)
            {
                //tnt.Detonate(true);
                tnt.StartTimer();
            }

            
        }
    }

    public void StartTimer()
    {
        if (!startTimer)
        {
            startTimer = true;
        }
    }

    private void HandleTimer()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                Detonate(true);
            }
        }
    }
}
