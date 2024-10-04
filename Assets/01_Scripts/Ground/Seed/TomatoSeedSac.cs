using UnityEngine;

namespace Jun.Ground.Crops
{
    public class TomatoSeedSac : SeedSacBase, IToolBase
    {
        private int _toolID;  // toolID 값을 저장할 필드

        public int toolID
        {
            get => _toolID;
            set => _toolID = value;
        }

        void Start()
        {
            toolID = 4;
        }


        public void DoAction(CultivationField targetField)
        {

        }

    }
}
