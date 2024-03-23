using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class DirtRow : MonoBehaviour
    {
        private CultivationField cultivationField;
        private MeshRenderer meshRenderer;

        [SerializeField] private List<PlantSocket> PlantSocketList = new();

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            cultivationField = GetComponentInParent<CultivationField>();
        }

        public void FullyWatered(Material wetMaterial)
        {
            meshRenderer.material = wetMaterial;
        }
    }
}