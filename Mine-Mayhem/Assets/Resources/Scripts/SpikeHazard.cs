using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHazard : MonoBehaviour
{
    [SerializeField] private float upForce = 5.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CustomPlayerController player = collision.gameObject.GetComponent<CustomPlayerController>();

        if(player != null)
        {
            if (!player.hitSpikeWall)
            {
                player.hitSpikeWall = !player.hitSpikeWall;
                player.DamagePlayer(player.curPlayerHealth);
                player.RB.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            }
        }
    }
}
