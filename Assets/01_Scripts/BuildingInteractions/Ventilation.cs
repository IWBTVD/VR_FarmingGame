using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingProps
{
    public class Ventilation : MonoBehaviour
    {
        [SerializeField] private float fanSpeed = 10f;
        private Transform fan;

        void Start()
        {
            fan = transform.GetChild(0);
        }

        void Update()
        {
            fan.Rotate(0, 0, fanSpeed * Time.deltaTime);
        }
    }
}

