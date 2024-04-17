using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class ShopNPC : NPCbase
    {
        /// <summary>
        /// 상호작용 UI
        /// </summary>
        public GameObject InteractionUI;
        public override void Interact()
        {
            if (InteractionUI != null)
                InteractionUI.SetActive(true);
        }

        public override void CloseUI()
        {
            if (InteractionUI != null)
                InteractionUI.SetActive(false);
        }
        public override void Talk()
        {
            StopCoroutine(TalkUICoroutine());
            StartCoroutine(TalkUICoroutine());

        }

        IEnumerator TalkUICoroutine()
        {
            talkUI.SetActive(true);
            Debug.Log("Interacting with ShopNPC");
            string talk1 = GetDialog("ShopNPC");
            TalkText.text = talk1;
            yield return new WaitForSeconds(2f);

            // 2초 후에 UI 창을 비활성화합니다.
            talkUI.SetActive(false);
        }

    }

}
