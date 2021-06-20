using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    //This objects collider
    //public Collider2D col;
    public CustomPlayerController miner { get { return GetComponentInParent<CustomPlayerController>(); } }
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
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            //change bool to reflect the detected wall
            if (!wallDetected)
            {
                wallDetected = !wallDetected;

            }

        }
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World") && collision.gameObject.tag != "TNT")
        {
            //change bool to reflect the detected wall
            if (!wallDetected)
            {
                wallDetected = !wallDetected;

            }

            // Small directional raycasts to detect which side the all is on
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.75f, worldLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.75f, worldLayer);

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
            //if theres a wall
            // && if neither left or right hit
            // canGrab == false

            //else canGrab ==  true
            if (!wallDetected || (!hitLeft && !hitRight))
            {
                    miner.canGrab = false;
            }
            else
            {
                miner.canGrab = true;
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
