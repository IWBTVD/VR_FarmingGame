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

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            cultivationField = GetComponentInParent<CultivationField>();
        }

        private void OnEnable()
        {
            meshRenderer.material = cultivationField.WetMaterial;
        }

        public void FullyWatered(Material wetMaterial)
        {
            meshRenderer.material = wetMaterial;
        }
    }
}