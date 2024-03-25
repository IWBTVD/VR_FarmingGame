using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{


    public class TestPlayer : MonoBehaviour
    {
        public GameObject mainCamera;

        private void Start()
        {

            mainCamera.gameObject.SetActive(true);

        }

        private void Update()
        {
            Move();
            CheckNPCbyRange();

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

        private void CheckNPCbyRange()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float range = 2f;
                Collider[] colls = Physics.OverlapSphere(transform.position, range);

                foreach (var coll in colls)
                {
                    if (coll.TryGetComponent<NPCbase>(out NPCbase npc))
                    {
                        npc.Interact();
                    }

                }
            }

        }
    }

}