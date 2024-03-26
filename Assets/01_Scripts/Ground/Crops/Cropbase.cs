using UnityEngine;
using Photon.Pun;

namespace Jun.Ground.Crops
{
    public class CropBase : MonoBehaviourPun
    {
        public float MAX_CULTIVATION_TIME;

        public enum CropsState
        {
            Seed,
            Sprout,
            ReadyForHarvest
        }
        public CropsState CurrentState;

        [SerializeField] protected GameObject[] _cropVisualList;
        
        public float growingTime;

        public virtual void ChangePotatoPrefab()
        {
            for (int i = 0; i < _cropVisualList.Length; i++)
            {
                _cropVisualList[i].SetActive(i == (int)CurrentState);
            }
        }

        public virtual void ChangeState(CropsState newState)
        {

            CurrentState = newState;
            ChangePotatoPrefab();

        }

        public virtual void CropGrowing()
        {
            if (CurrentState == CropsState.ReadyForHarvest)
            {
                return;
            }

            growingTime += Time.deltaTime;

            /*
            if (cultivationTime >= MAX_CULTIVATION_TIME / 2 && CurrentState == CropsState.Sprout)
            {
                ChangeState(CropsState.Growing);
            }
            else if (cultivationTime >= MAX_CULTIVATION_TIME && CurrentState == CropsState.Growing)
            {
                ChangeState(CropsState.Harvest);
            }
            */
        }

        public virtual void HarvestCrops()
        {

            switch (CurrentState)
            {
                /*
                case CropsState.Sprout:
                    Debug.Log("아직 열리지 않음");
                    break;
                case CropsState.Growing:
                    Debug.Log("한개 떨어짐");
                    break;
                */
                case CropsState.ReadyForHarvest:
                    Debug.Log("모두 떨어짐");
                    break;
            }


            CropPoint cropPoint = GetComponentInParent<CropPoint>();
            cropPoint.Harvest();
            Destroy(gameObject);
        }


        public void OnPhotonSerializeView(PhotonStream stream)
        {
            /*
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentState);
                stream.SendNext(cultivationTime);
            }
            else
            {
                CurrentState = (CropsState)stream.ReceiveNext();
                ChangeState(CurrentState);
                cultivationTime = (float)stream.ReceiveNext();
            }
            */
        }
    }
}