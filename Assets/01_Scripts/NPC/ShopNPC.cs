using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class ShopNPC : NPCbase
    {
        public override void Interact()
        {
            Debug.Log("Interacting with ShopNPC");
            string talk1 = "Hello, I am a ShopNPC";
            TalkText.text = talk1;
        }
    }

}
