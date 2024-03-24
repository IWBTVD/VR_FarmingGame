using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 경작 가능한 땅
    /// </summary>
    public class CultivationField : MonoBehaviour
    {
        [SerializeField] private bool _isWatered = false;
        /// <summary>
        /// 물을 준 상태인지 여부. 물을 가득 준 상태라면 색깔이 짙어지고, 재배한 식물이 자라난다.
        /// </summary>
        public bool IsWatered => _isWatered;

        [SerializeField] private bool _canPlowed = false;
        /// <summary>
        /// 경작을 할 수 있는지 여부. true라면 쇠스랑으로 경작하여 둑을 만들 수 있다.
        /// </summary>
        public bool CanPlowed => _canPlowed;

        [SerializeField] private bool _isPlowed = false;
        /// <summary>
        /// 경작이 되어 둑이 생성되었는지 여부. true라면 식물을 만든 둑에 재배할 수 있다.
        /// </summary>
        public bool IsPlowed => _isPlowed;


        [Space()]
        [SerializeField] private Material dryMaterial;
        [SerializeField] private Material wetMaterial;
        public Material WetMaterial => wetMaterial;

        [Space()]
        [SerializeField] private DirtRow dirtRow;
        [SerializeField] private List<IObstacle> obstacleList = new();

        private MeshRenderer meshRenderer;

        private int wateringAmount = 0;
        private int plowedAmount = 0;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            dirtRow.gameObject.SetActive(false);
        }

        /// <summary>
        /// 땅에 물주기
        /// </summary>
        /// <param name="amount">물주는 양</param>
        public void WaterGround(int amount)
        {
            if (_isWatered) return;

            wateringAmount += amount;

            if (wateringAmount >= 100) FullyWatered();
        }

        /// <summary>
        /// 땅에 물을 흠뻑 줌
        /// </summary>
        public void FullyWatered()
        {
            _isWatered = true;
            meshRenderer.material = wetMaterial;
            dirtRow.FullyWatered(wetMaterial);
        }

        /// <summary>
        /// 땅 갈기
        /// </summary>
        public void PlowGround(int amount)
        {
            if (_isPlowed) return;

            plowedAmount += amount;
            if(plowedAmount >= 100)
            {
                FullyPlowed();
            }
        }

        /// <summary>
        /// 땅을 충분히 경작함
        /// </summary>
        public void FullyPlowed()
        {
            _isPlowed = true;
            dirtRow.gameObject.SetActive(true);
        }
    }
}
