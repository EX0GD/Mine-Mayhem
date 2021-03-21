using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public bool detonate;
    public float explosionRadius;
    public float explosionForce;

    // Start is called before the first frame update
    void Start()
    {
        detonate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detonate()
    {
        if (detonate)
        {
            // Explosion stuff happens here
        }
    }
}
