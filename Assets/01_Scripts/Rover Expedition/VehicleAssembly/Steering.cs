using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Autohand;


public class Steering
{

    public float H { get; private set; }
    public float V { get; private set; }
    public bool Cruising { get; private set; } // cruise control
    public bool mouse_hold;
    public float mouse_start;

    // private TestJoystick testJoystick;

    public Steering()
    {
        // this.testJoystick = testJoystick;
    }

    // Use this for initialization
    public void Start()
    {
        H = 0f;
        V = 0f;
        Cruising = false;
        mouse_hold = false;
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
            // 수직을 입력 받음
            // V = testJoystick.GetKeyboardInputY();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (H > -1.0)
            {
                H -= 0.05f;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (H < 1.0)
            {
                H += 0.05f;
            }
        }

        // if (testJoystick.GetKeyboardInputX() < 0)
        // {
        //     if (H > -1.0)
        //     {
        //         H -= 0.05f;
        //     }
        // }
        // else if (testJoystick.GetKeyboardInputX() > 0)
        // {
        //     if (H < 1.0)
        //     {
        //         H += 0.05f;
        //     }
        // }

    }
}

