using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    //This objects collider
    //public Collider2D col;

    //world layer mask
    public LayerMask worldLayer;

    //bools for checking the wall
    public bool wallDetected = false;
    public bool onLeft = false;
    /*
    void Start() {
       
        col = this.GetComponent<Collider2D>();
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            //change bool to reflect the detected wall
            if (!wallDetected)
            {
                wallDetected = !wallDetected;

            }

            // Small directional raycasts to detect which side the all is on
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, worldLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, worldLayer);

            if (hitLeft)
            {
                if (!onLeft)
                {
                    onLeft = !onLeft;
                }
            }
            else if (hitRight)
            {
                if (onLeft)
                {
                    onLeft = !onLeft;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            //there is no longer a wall there, reset bools
            if (wallDetected)
            {
                wallDetected = !wallDetected;
            }
             
            if (onLeft)
            {
                onLeft = !onLeft;
            }
        }
    }

}
