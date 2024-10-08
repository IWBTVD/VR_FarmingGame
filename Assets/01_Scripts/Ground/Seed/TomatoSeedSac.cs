using EPOOutline;
using UnityEngine;

namespace Jun.Ground.Crops
{
    public class TomatoSeedSac : SeedSacBase, IPlantBase, IToolBase
    {
        private int _toolID;  // toolID 값을 저장할 필드


        [Header("Soils")]
        [SerializeField] private LayerMask CropPointLayers;
        [SerializeField] private float CropPointOffset;
        [SerializeField] private float CropPointRadius;


        private bool isCrop;
        private Outlinable _outlinable;

        public int toolID
        {
            get => _toolID;
            set => _toolID = value;
        }
        private CropPoint _nearCropPoint;
        public CropPoint cropPoint
        {
            get => _nearCropPoint;
            set => _nearCropPoint = value;
        }


        protected override void Awake()
        {
            _outlinable = GetComponent<Outlinable>();
        }
        void Start()
        {
            toolID = 3;

            _outlinable.OutlineParameters.Enabled = false;
        }

        void Update()
        {
            CropPointCheck();
        }


        void CropPointCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - CropPointOffset, transform.position.z);

            Collider[] hitColliders = Physics.OverlapSphere(spherePosition, CropPointRadius, CropPointLayers, QueryTriggerInteraction.Ignore);

            if (_nearCropPoint != null)
                _nearCropPoint._outlinable.OutlineParameters.Enabled = false;

            _nearCropPoint = null;

            if (hitColliders.Length > 0)
            {
                isCrop = true;

                foreach (Collider hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider.name);

                    if (hitCollider.CompareTag("CropPoint"))
                    {
                        Debug.Log("Near CropPoint");
                        _nearCropPoint = hitCollider.gameObject.GetComponent<CropPoint>();
                        if (!_nearCropPoint.IsPlanted)
                            _nearCropPoint._outlinable.OutlineParameters.Enabled = true;
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
        public void DoAction()
        {
            if (_nearCropPoint == null) return;
            _nearCropPoint.PlantCrop(this);
        }

        public void SetCropPoint(CropPoint targetCropPoint)
        {
            _nearCropPoint = targetCropPoint;
        }
    }
}
