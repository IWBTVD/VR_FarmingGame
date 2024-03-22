using Photon.Pun;
using UnityEngine;

namespace Jun.Ground.Crops
{
    public class Corn : Cropbase
    {
        public void Start()
        {
            pv = GetComponent<PhotonView>();

            CurrentState = CropsState.Sprout;
            MAXCULTOVATIONTIME = 10f;

            // Setup the CrobsPrefabs array
            CropsPrefabs = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                CropsPrefabs[i] = transform.GetChild(i).gameObject;
                CropsPrefabs[i].SetActive(false);
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
            if (pv.IsMine && other.gameObject.CompareTag("HarvestTool"))
            {
                HarvestCrops();
            }
        }

        private void TestCode()
        {
            if (pv.IsMine)
            {
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
            }
        }

    }
}
