using Jun.Ground.Crops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun 
{
    /// <summary>
    /// 식물 베이스
    /// </summary>
    public class PlantBase : MonoBehaviour
    {
        #region 변수들
        [SerializeField] protected PlantSO _plantSO;
        [SerializeField] protected List<GameObject> sproutVisualList;
        [SerializeField] protected GameObject matureVisual;
        
        protected CropPoint _cropPoint;
        protected CultivationField _cultivationField;
        protected int _dayPassed = 0;
        protected int _growthDays = 0;
        #endregion

        public PlantSO PlantSO => _plantSO;
        public CropPoint CropPoint => _cropPoint;
        public CultivationField CultivationField
        {
            get
            {
                return _cultivationField;
            }
        }
        public bool IsWatered => CultivationField.IsWatered;
        /// <summary>
        /// 작물이 심어지고 난 뒤 경과한 일 수
        /// </summary>
        public int DayPassed => _dayPassed;
        /// <summary>
        /// 작물이 성장한 일 수
        /// </summary>
        public int GrowthDays => _growthDays;

        protected virtual void Start()
        {
            OnPlanted();
        }

        /// <summary>
        /// 막 심어졌을 때 메소드
        /// </summary>
        public virtual void OnPlanted()
        {
            foreach(var sprout in sproutVisualList)
            {
                sprout.gameObject.SetActive(false);
            }
            matureVisual.SetActive(false);
            sproutVisualList[0].SetActive(true);


        }

        /// <summary>
        /// 하루가 경과했을 때 호출되는 메소드
        /// </summary>
        /// <param name="day">기본값은 1이고, 혹여나 하루 이상이 흘러갔을 경우를 위하여 만들어놓음(쓸일 없을 듯)</param>
        public virtual void OnDayPassed(int day = 1)
        {
            _dayPassed += day;

            if (IsWatered) _growthDays += 1;
        }
    }
    
}
