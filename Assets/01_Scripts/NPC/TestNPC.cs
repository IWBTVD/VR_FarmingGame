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
        public override void Talk()
        {
            StopCoroutine(CoroutineName());
            StartCoroutine(CoroutineName());

        }

        IEnumerator CoroutineName()
        {
            talkUI.SetActive(true);
            Debug.Log("Interacting with TestNPC");
            string talk1 = GetDialog("Pedestrian");
            Debug.Log(talk1);
            TalkText.text = talk1;
            yield return new WaitForSeconds(2f);

            // 2초 후에 UI 창을 비활성화합니다.
            talkUI.SetActive(false);
        }
    }

}
