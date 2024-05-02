using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{


    public class TestPlayer : MonoBehaviour
    {
        public GameObject mainCamera;
        private GameObject nearbyNPC;
        public EntranceMediator entrance;


        private void Start()
        {

            mainCamera.gameObject.SetActive(true);

        }

        private void Update()
        {
            Move();
            DetectNPCbyRange();
            InteractionNPCUI();
            DeleteNPCbyRange();
            TalkWithNPC();
            EnterRoon();
        }

        private void EnterRoon()
        {
            if (Input.GetKeyDown(KeyCode.F))
                entrance.EnterRoon();
        }
        private void InteractionNPCUI()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (nearbyNPC != null)
                {
                    nearbyNPC.GetComponent<NPCbase>().Interact();
                }
            }
        }

        //마우스 위치에따라 카메라 회전

        private void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * 2);
            mainCamera.transform.Rotate(Vector3.right, -mouseY * 2);
        }

        //wasd로 3인칭 캐릭터 이동 함수
        private void Move()
        {
            float moveSpeed = 2f;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(x, 0, z);
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        }

        private void TalkWithNPC()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(nearbyNPC);
                if (nearbyNPC != null)
                {
                    nearbyNPC.GetComponent<NPCbase>().Talk();
                }
            }
        }

        private void DetectNPCbyRange()
        {
            float range = 2f;
            Collider[] colls = Physics.OverlapSphere(transform.position, range);

            foreach (var coll in colls)
            {
                if (coll.TryGetComponent<NPCbase>(out NPCbase npc))
                {
                    nearbyNPC = coll.gameObject;
                }
            }
        }


        private void DeleteNPCbyRange()
        {
            if (nearbyNPC != null)
            {
                if (Vector3.Distance(transform.position, nearbyNPC.transform.position) > 4f)
                {
                    nearbyNPC.GetComponent<NPCbase>().CloseUI();
                    nearbyNPC = null;
                }
            }
        }
    }

}