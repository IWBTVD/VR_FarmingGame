using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jun.Ground.Crops;

namespace Gun
{
    public class MoundsVisual : MonoBehaviour
    {
        private CultivationField cultivationField;
        private MeshRenderer meshRenderer;

        [SerializeField] private Material dryMaterial;
        [SerializeField] private Material wetMaterial;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            cultivationField = GetComponentInParent<CultivationField>();
        }

        private void OnEnable()
        {
            meshRenderer.material = cultivationField.IsWatered? wetMaterial : dryMaterial;
        }

        public void FullyWatered()
        {
            meshRenderer.material = wetMaterial;
        }
    }
}