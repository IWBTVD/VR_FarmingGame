using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class ShopNPC : NPCbase
    {

        public override void Interact()
        {
            StopCoroutine(CoroutineName());
            StartCoroutine(CoroutineName());

        }

        IEnumerator CoroutineName()
        {
            gameObjectUI.SetActive(true);
            Debug.Log("Interacting with ShopNPC");
            string talk1 = "Hello, I am a ShopNPC";
            TalkText.text = talk1;
            yield return new WaitForSeconds(2f);

            // 2초 후에 UI 창을 비활성화합니다.
            gameObjectUI.SetActive(false);
        }

    }

}
