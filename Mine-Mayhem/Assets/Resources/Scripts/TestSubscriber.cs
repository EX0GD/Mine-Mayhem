using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DelegateTest delTest = FindObjectOfType<DelegateTest>();
        if(delTest != null)
        {
            delTest.OnSpacePressed += Testing_OnSpacePressed;
        }
        else
        {
            Debug.Log("Could not find DelegateTest.");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Testing_OnSpacePressed(object s, DelegateTest.OnSpacePressedEventArgs e)
    {
        Debug.Log(e.myTestEventArgsFloat);
        Debug.Log("Just called our first event from our first" + this);
    }
}
