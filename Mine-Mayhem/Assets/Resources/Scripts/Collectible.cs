using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        GOLD,
        GEM, 
        NONE
    }
    [SerializeField] private CollectibleType PickUpType = CollectibleType.NONE;
    public CollectibleType Type { get { return PickUpType; } }

    public static event Action<Collectible, CollectibleType> OnPickUpCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CustomPlayerController>() != null)
        {
            OnPickUpCollectible?.Invoke(this, PickUpType);
            Destroy(gameObject);
        }
    }
}
