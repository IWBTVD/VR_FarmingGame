using UnityEngine;

namespace Jun.Ground.Crops
{
    public class TomatoSeedSac : SeedSacBase, IPlantBase
    {
        private int _toolID;  // toolID 값을 저장할 필드

        [SerializeField] private CropPoint testPoint;

        [Header("Soils")]
        [SerializeField] private LayerMask CropPointLayers;
        [SerializeField] private float CropPointOffset;
        [SerializeField] private float CropPointRadius;


        private CropPoint _nearCropPoint;
        private bool isCrop;

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

            CropPointCheck();

        }

        public void Action()
        {
            if (_nearCropPoint == null) return;

            DoAction(_nearCropPoint);
        }

        void CropPointCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - CropPointOffset, transform.position.z);

            Collider[] hitColliders = Physics.OverlapSphere(spherePosition, CropPointRadius, CropPointLayers, QueryTriggerInteraction.Ignore);

            _nearCropPoint = null;

            if (hitColliders.Length > 0)
            {
                isCrop = true;
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("CropPoint"))
                    {
                        _nearCropPoint = hitCollider.gameObject.GetComponent<CropPoint>();
                        break;
                    }
                }
            }
            else
            {
                isCrop = false;
            }
        }

        void OnDrawGizmos()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - CropPointOffset, transform.position.z);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spherePosition, CropPointRadius);

        }

        private void DoAction(CropPoint cropPoint)
        {

            cropPoint.PlantCrop(this);
        }


    }
}
