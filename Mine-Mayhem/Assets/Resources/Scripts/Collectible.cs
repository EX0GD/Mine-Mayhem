using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int value;
    public static event Action OnPickUpCollectible;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CustomPlayerController>() != null)
        {
            //Debug.Log("Calling 'OnPickUpCollectible' in Collectible script.");
            OnPickUpCollectible?.Invoke();
            Destroy(gameObject);
        }
    }
}
