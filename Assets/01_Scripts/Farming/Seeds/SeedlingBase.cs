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

        protected SeedlingTrayBase tray;
        protected CropPoint currentCropPoint;
        protected SeedlingRoot seedlingRoot;
        protected Grabbable grabbable;
        #endregion
        public PlantBase PlantPrefab => _plantPrefab;

        protected virtual void Awake()
        {
            grabbable = GetComponent<Grabbable>();

            tray ??= GetComponentInParent<SeedlingTrayBase>();
            grabbable.onGrab.AddListener(OnFirstGrab);
        }

        public void RootPlanted(CropPoint cropPoint)
        {
            this.currentCropPoint = cropPoint;
        }

        public void RootUnplanted(CropPoint cropPoint)
        {
            if(this.currentCropPoint == cropPoint)
        
                this.currentCropPoint = null;
        }

        public void OnRelease(Hand hand, Grabbable grabbable)
        {
            //cropPoint에 뿌리를 심은 채로 놓았으면 작물이 심어진다
            if(currentCropPoint != null)
            {
                currentCropPoint.PlantCrop(_plantPrefab);

                //이 모종은 삭제된다
                gameObject.SetActive(false);
                Destroy(gameObject, 1f);
            }
            else
            {
                //손에서 놓으면 부모로부터 독립함
                transform.parent = null;
            }
        }

        public void OnFirstGrab(Hand hand, Grabbable grabbable)
        {
            if (tray == null) return;

            grabbable.originalParent = null;
            tray = null;

            grabbable.onGrab.RemoveListener(OnFirstGrab);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("EnterTrigger");
            if (other.TryGetComponent(out CropPoint cropPoint))
            {
                currentCropPoint = cropPoint;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "CropPoint")
            {
                currentCropPoint = null;
            }
        }
    }
}