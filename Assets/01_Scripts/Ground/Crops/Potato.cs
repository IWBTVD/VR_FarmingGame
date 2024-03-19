using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace Jun.Ground.Crops
{
    public class Potato : MonoBehaviourPunCallbacks, ICrops, IPunObservable
    {
        public CropsState CurrentState { get; set; } = CropsState.Sprout;
        public GameObject[] CropsPrefabs { get; set; }
        public float MAXCULTOVATIONTIME { get; set; } = 10f;
        public float CultivationTime { get; set; } = 0f;

        private PhotonView pv;

        public void Start()
        {
            pv = GetComponent<PhotonView>();

            // Setup the CrobsPrefabs array
            CropsPrefabs = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                CropsPrefabs[i] = transform.GetChild(i).gameObject;
                CropsPrefabs[i].SetActive(false);
            }
            ChangePrefab();
        }

        public void Update()
        {
            if (pv.IsMine)
            {
                CropGrowing();

                TestCode();
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

        public void ChangePrefab()
        {
            for (int i = 0; i < CropsPrefabs.Length; i++)
            {
                CropsPrefabs[i].SetActive(i == (int)CurrentState);
            }
        }

        public void ChangeState(CropsState newState)
        {
            if (pv.IsMine)
            {
                CurrentState = newState;
                pv.RPC("ChangePrefab", RpcTarget.AllBuffered);
            }
        }

        public void CropGrowing()
        {
            CultivationTime += Time.deltaTime;

            if (CultivationTime >= MAXCULTOVATIONTIME / 2)
            {
                ChangeState(CropsState.Growing);
            }
            else if (CultivationTime >= MAXCULTOVATIONTIME)
            {
                ChangeState(CropsState.Harvest);
            }
        }

        public void HarvestCrops()
        {
            if (pv.IsMine)
            {
                switch (CurrentState)
                {
                    case CropsState.Sprout:
                        Debug.Log("아직 열리지 않음");
                        pv.RPC("DestroyCrops", RpcTarget.AllBuffered);
                        break;
                    case CropsState.Growing:
                        Debug.Log("한개 떨어짐");
                        pv.RPC("DestroyCrops", RpcTarget.AllBuffered);
                        break;
                    case CropsState.Harvest:
                        Debug.Log("모두 떨어짐");
                        pv.RPC("DestroyCrops", RpcTarget.AllBuffered);
                        break;
                }
            }
        }

        [PunRPC]
        private void DestroyCrops()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (pv.IsMine && other.gameObject.tag == "HarvestTool")
            {
                HarvestCrops();
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentState);
                stream.SendNext(CultivationTime);
            }
            else
            {
                CurrentState = (CropsState)stream.ReceiveNext();
                CultivationTime = (float)stream.ReceiveNext();
            }
        }
    }
}
