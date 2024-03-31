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
        /// <summary>
        /// 대화창 UI
        /// </summary>
        public GameObject talkUI;
        public TextMeshProUGUI TalkText;
        private String talk1;


        // Start is called before the first frame update
        void Start()
        {
            talkUI.SetActive(false);
        }

        void Update()
        {

        }
        /// <summary>
        /// 대화창을 띄우는 함수
        /// </summary>
        public virtual void Talk()
        {
            Debug.Log("Interacting with base class");
        }
        /// <summary>
        /// 상호작용 함수
        /// </summary>
        public virtual void Interact()
        {
            Debug.Log("No interaction");
        }

        /// <summary>
        /// 거리가 멀어지면 대화창을 닫는 함수
        /// </summary>
        public virtual void CloseUI()
        {
            Debug.Log("No UI to close");
        }

        /// <summary>
        /// 대화창을 열어주는 함수
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public virtual string GetDialog(string eventName)
        {
            return DialogManager.Instance.GetDialog(eventName);
        }

    }
}


