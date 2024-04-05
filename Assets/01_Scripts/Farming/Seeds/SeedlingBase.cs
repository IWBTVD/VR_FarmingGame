using Jun.Ground.Crops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

namespace Gun
{
    /// <summary>
    /// 모종으로 심는 작물들의 베이스 클래스
    /// </summary>
    public class SeedlingBase : MonoBehaviour
    {
        #region PROPERTIES
        [SerializeField] protected PlantBase _plantPrefab;

        protected CropPoint cropPoint;
        protected SeedlingRoot seedlingRoot;
        protected Grabbable grabbable;
        #endregion
        public PlantBase PlantPrefab => _plantPrefab;
        public bool IsPlanted => cropPoint != null;

        protected virtual void Awake()
        {
            grabbable = GetComponent<Grabbable>();
        }

        public void RootPlanted(CropPoint cropPoint)
        {
            this.cropPoint = cropPoint;
        }

        public void RootUnplanted(CropPoint cropPoint)
        {
            if(this.cropPoint == cropPoint)
                this.cropPoint = null;
        }

        public void OnRelease(Hand hand, Grabbable grabbable)
        {
            //cropPoint에 뿌리를 심은 채로 놓았으면 작물이 심어진다
            if(IsPlanted)
            {
                cropPoint.PlantCrop(_plantPrefab);

                //이 모종은 삭제된다
                gameObject.SetActive(false);
                Destroy(gameObject, 3f);
            }
            else
            {
                //손에서 놓으면 부모로부터 독립함
                transform.parent = null;
            }
        }
    }
}