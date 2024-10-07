using UnityEngine;

namespace Jun.Ground.Crops
{
    public class TomatoSeedSac : SeedSacBase, IPlantBase
    {
        private int _toolID;  // toolID 값을 저장할 필드

        [SerializeField] private CropPoint testPoint;

        public int toolID
        {
            get => _toolID;
            set => _toolID = value;
        }

        void Start()
        {
            toolID = 4;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                DoAction(testPoint);
            }

        }



        public void DoAction(CropPoint cropPoint)
        {

            cropPoint.PlantCrop(this);
        }


    }
}
