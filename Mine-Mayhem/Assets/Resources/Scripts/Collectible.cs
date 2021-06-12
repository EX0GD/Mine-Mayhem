using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Collectible : MonoBehaviour
{
    private AudioSource CollectibleAudioSource { get { return GetComponent<AudioSource>(); } }
    private SpriteRenderer CollectibleRenderer { get { return GetComponent<SpriteRenderer>(); } }
    private CircleCollider2D CollectibleCollider { get { return GetComponent<CircleCollider2D>(); } }
    private Light2D CollectibleLight { get { return GetComponent<Light2D>(); } }

    private bool IsGemCollected { get; set; }

    public enum CollectibleType
    {
        GOLD,
        GEM, 
        NONE
    }
    [SerializeField] private CollectibleType PickUpType = CollectibleType.NONE;
    public CollectibleType Type { get { return PickUpType; } }

    public static event Action<Collectible, CollectibleType> OnPickUpCollectible;

    private void Start()
    {
        IsGemCollected = false;
    }

    private void Update()
    {
        HandleLateGemDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CustomPlayerController>() != null)
        {
            //SoundManager.PlaySound(gemPickup);
            if(Type == CollectibleType.GEM)
            {
                //SoundManager.PlaySound(SoundManager.GemPickup);
                CollectibleAudioSource.Play();
                DisableBeforeDestroy(true);
            }
            else
            {
                Destroy(gameObject);
            }
            
            OnPickUpCollectible?.Invoke(this, PickUpType);
        }
    }

    private void DisableBeforeDestroy(bool value)
    {
        if(CollectibleRenderer.enabled == value && CollectibleCollider.enabled == value)
        {
            CollectibleRenderer.enabled = !value;
            CollectibleCollider.enabled = !value;
            CollectibleLight.enabled = !value;
            IsGemCollected = value;
        }
    }

    private void HandleLateGemDestroy()
    {
        if (IsGemCollected)
        {
            if (CollectibleAudioSource.isPlaying)
            {
                if(CollectibleAudioSource.time > 0.5f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
