using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class NPCbase : MonoBehaviour
    {
        public GameObject gameObjectUI;
        public TextMeshProUGUI TalkText;

        // Start is called before the first frame update
        void Start()
        {
            gameObjectUI.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Interact()
        {
            Debug.Log("Interacting with base class");
        }
    }

}


