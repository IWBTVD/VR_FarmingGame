using UnityEngine;
using Photon.Pun;
using System.Collections;
using Gun;

namespace Jun.Ground.Crops
{
    public class CropPoint : MonoBehaviour
    {
        private CultivationField _cultivationField;
        public CultivationField CultivationField
        {
            get
            {
                if (_cultivationField == null) _cultivationField = GetComponentInParent<CultivationField>();
                return _cultivationField;
            }
        }
        public bool IsWatered => CultivationField.IsWatered;

        [SerializeField] private PlantBase _plant;
        public PlantBase Plant => _plant;
        public bool IsPlanted => _plant != null;

        private CropMound rowCropsGround;

        private void Start()
        {
            rowCropsGround = GetComponentInParent<CropMound>();
        }

        private void Update()
        {
        }

        public void ReceiveSeed(SeedBase IncomeSeed)
        {

        }

        /// <summary>
        /// 작물 심기
        /// </summary>
        /// <param name="seed"></param>
        public void PlantCrop(SeedBase seed)
        {

        }

        public void PlantCrop(PlantBase plantBase)
        {
            _plant = Instantiate(plantBase, transform);
        }

        private void OnDayPassed()
        {
            //_plant
        }

        public void Harvest()
        {
            //rowCropsGround.NotifyRemoveCrop(this.gameObject, seedlings);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Detected");
        }
    }
}
