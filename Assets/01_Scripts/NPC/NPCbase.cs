using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jun
{
    public class NPCbase : MonoBehaviourPun
    {
        public GameObject talkUI;
        public TextMeshProUGUI TalkText;
        private String talk1;


        // Start is called before the first frame update
        void Start()
        {
            talkUI.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void Talk()
        {
            Debug.Log("Interacting with base class");
        }

        public virtual void Interact()
        {
            Debug.Log("No interaction");
        }

        public virtual void CloseUI()
        {
            Debug.Log("No UI to close");
        }

        public virtual string GetDialog(string eventName)
        {
            return DialogManager.Instance.GetDialog(eventName);
        }

    }
}


