using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class UITester : MonoBehaviour
    {
        public GameObject nerbyNPC;
        public int money = 0;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            DetectNPCbyRange();
            DeleteNPCbyRange();

        }

        private void DetectNPCbyRange()
        {
            float range = 2f;
            Collider[] colls = Physics.OverlapSphere(transform.position, range);

            foreach (var coll in colls)
            {
                if (coll.TryGetComponent<NPCbase>(out NPCbase npc))
                {
                    nerbyNPC = coll.gameObject;
                }
            }
        }

        private void DeleteNPCbyRange()
        {
            if (nerbyNPC != null)
            {
                if (Vector3.Distance(transform.position, nerbyNPC.transform.position) > 4f)
                {
                    nerbyNPC.GetComponent<NPCbase>().CloseUI();
                    nerbyNPC = null;
                }
            }
        }

        public void TalkWithNPC()
        {
            if (nerbyNPC != null)
            {
                nerbyNPC.GetComponent<NPCbase>().Talk();
            }
        }

        public void InteractWithNPC()
        {
            if (nerbyNPC != null)
            {
                nerbyNPC.GetComponent<NPCbase>().Interact();
            }
        }
    }

}
