using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Jun
{
    public class TestNPC : NPCbase
    {
        public override void Interact()
        {
            Debug.Log("Interacting with TestNPC");
            String talk1 = "Hello, I am a TestNPC";
            TalkText.text = talk1;
        }
    }

}
