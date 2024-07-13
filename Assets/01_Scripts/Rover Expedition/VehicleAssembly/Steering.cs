using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Steering
{

    public float H { get; private set; }
    public float V { get; private set; }
    public bool Cruising { get; private set; } // cruise control

    // Use this for initialization
    public void Start()
    {
        H = 0f;
        V = 0f;
        Cruising = false;
    }

    // Update is called once per frame
    public void UpdateValues()
    {
        // Cruise Control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cruising = !Cruising;
        }

        if (Cruising)
        {
            V = 0.4f; // gets to max speed at a gradual pace
        }
        else
        {
            V = CrossPlatformInputManager.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (H > -1.0)
            {
                H -= 0.05f;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (H < 1.0)
            {
                H += 0.05f;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // get the mouse position
            float mousePosition = Input.mousePosition.x;


        }

    }
}
