using Photon.Pun;
using UnityEngine;

namespace Jun.Ground.Crops
{
    public class Tomato : CropBase
    {
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

    }
}
       