using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour
{

    public class OnSpacePressedEventArgs : EventArgs
    {
        public float myTestEventArgsFloat = 30.0f;
    }
    
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { myTestEventArgsFloat = 30.0f });
        }
    }
}
