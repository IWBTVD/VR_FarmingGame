using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gun
{
    public class SandBagDummy : MonoBehaviour
    {
        [SerializeField] private Collider dummyCollider;
        [SerializeField] private TextMeshProUGUI velocityLabel;

        private void Awake()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            velocityLabel.text = collision.relativeVelocity.magnitude.ToString();
        }
    }
}
