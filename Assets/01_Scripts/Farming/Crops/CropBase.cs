using UnityEngine;
using Photon.Pun;
using Autohand;

namespace Jun.Ground.Crops
{
    public class CropBase : MonoBehaviourPun
    {
        public float MAX_CULTIVATION_TIME;

        /// <summary>
        /// 현재까지 성장한 타이머
        /// </summary>
        protected float growthTimer = 0f;
        /// <summary>
        /// 최대 성장 시간
        /// </summary>
        protected float maxGrowthTime;
        protected float fruitingTimer;
        protected float fruitingTime;

        protected Grabbable _grabbable;
        protected Grabbable Grabbable => _grabbable;

        public enum CropsState
        {
            /// <summary>
            /// 씨앗
            /// </summary>
            Seed,
            /// <summary>
            /// 싹
            /// </summary>
            Sprout,
            /// <summary>
            /// 성숙함
            /// </summary>
            Mature,
        }
        public CropsState CurrentState = CropsState.Seed;

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
            if (CurrentState == CropsState.Mature)
            {
                return;
            }

            growingTime += Time.deltaTime;
        }

        public virtual void HarvestCrops()
        {

            switch (CurrentState)
            {
                case CropsState.Mature:
                    Debug.Log("모두 떨어짐");
                    break;
            }


            CropPoint cropPoint = GetComponentInParent<CropPoint>();
            cropPoint.Harvest();
            //Destroy(gameObject);
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