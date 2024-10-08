using UnityEngine;
using Photon.Pun;
using System.Collections;
using EPOOutline;

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
        public bool IsPlanted;
        public Outlinable _outlinable;

        private CropMound rowCropsGround;

        private void Start()
        {
            rowCropsGround = GetComponentInParent<CropMound>();

            _outlinable = GetComponentInChildren<Outlinable>();

            _outlinable.OutlineParameters.Enabled = false;
        }

        private void Update()
        {
        }

        public void ReceiveSeed(SeedSacBase IncomeSeed)
        {

        }

        /// <summary>
        /// 작물 심기
        /// </summary>
        /// <param name="seed"></param>
        public void PlantCrop(SeedSacBase seed)
        {
            if (IsPlanted) return;
            SeedBase seedPrefab = seed.GetSeedBase();
            PlantBase plantPrefab = seedPrefab.GetPlantPrefab();
            _plant = Instantiate(plantPrefab, transform);
            IsPlanted = true;

            BehaviourManager.Instance.AddSowedCount();
        }

        public void PlantCrop(PlantBase plantBase)
        {
            if (IsPlanted) return;
            _plant = Instantiate(plantBase, transform);
            IsPlanted = true;
            _outlinable.OutlineParameters.Enabled = false;

            BehaviourManager.Instance.AddSowedCount();

        }


        public void Harvest()
        {
            //rowCropsGround.NotifyRemoveCrop(this.gameObject, seedlings);
        }
    }
}
