using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Jun.Ground.Crops
{
    public class Potato : CropBase
    {
        [SerializeField] protected List<GameObject> bulbsList = new();

        public void Start()
        {
            CurrentState = CropsState.Sprout;
            MAX_CULTIVATION_TIME = 10f;

            // Setup the CrobsPrefabs array
            _cropVisualList = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _cropVisualList[i] = transform.GetChild(i).gameObject;
                _cropVisualList[i].SetActive(false);
            }
            ChangePotatoPrefab();
        }

        public void Update()
        {
            CropGrowing();

            // TestCode();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine && other.gameObject.CompareTag("HarvestTool"))
            {
                HarvestCrops();
            }
        }

        private void TestCode()
        {
            if (photonView.IsMine)
            {
                /*
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(CropsState.Sprout);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(CropsState.Growing);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChangeState(CropsState.Harvest);
                }
                */
            }
        }

    }
}
