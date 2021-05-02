using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        GOLD,
        GEM
    }
    [SerializeField] private CollectibleType PickUpType;

    public static event Action<CollectibleType> OnPickUpCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CustomPlayerController>() != null)
        {
            //Debug.Log("Calling 'OnPickUpCollectible' in Collectible script.");
            OnPickUpCollectible?.Invoke(PickUpType);
            Destroy(gameObject);
        }
    }
}
