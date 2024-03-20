using UnityEngine;
using Photon.Pun;

namespace Jun.Ground.Crops
{
    public class Cropbase : MonoBehaviourPunCallbacks
    {
        public enum CropsState
        {
            Sprout,
            Growing,
            Harvest
        }
        public CropsState CurrentState;
        public GameObject[] CropsPrefabs;
        public float MAXCULTOVATIONTIME;
        public float CultivationTime;

        public PhotonView pv;
                
        public virtual void ChangePotatoPrefab()
        {
            for (int i = 0; i < CropsPrefabs.Length; i++)
            {
                CropsPrefabs[i].SetActive(i == (int)CurrentState);
            }
        }

        public virtual void ChangeState(CropsState newState)
        {

            CurrentState = newState;
            // pv.RPC(nameof(ChangePotatoPrefab), RpcTarget.AllBuffered);
            ChangePotatoPrefab();
            
        }

        public virtual void CropGrowing()
        {
            if (CurrentState == CropsState.Harvest)
            {
                return;
            }

            CultivationTime += Time.deltaTime;

            if (CultivationTime >= MAXCULTOVATIONTIME / 2 && CurrentState == CropsState.Sprout)
            {
                ChangeState(CropsState.Growing);
            }
            else if (CultivationTime >= MAXCULTOVATIONTIME && CurrentState == CropsState.Growing)
            {
                ChangeState(CropsState.Harvest);
            }
        }

        public virtual void HarvestCrops()
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

        public void OnPhotonSerializeView(PhotonStream stream)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentState);
                stream.SendNext(CultivationTime);
            }
            else
            {
                CurrentState = (CropsState)stream.ReceiveNext();
                ChangeState(CurrentState);
                CultivationTime = (float)stream.ReceiveNext();
            }
        }
    }
}