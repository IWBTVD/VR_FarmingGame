using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class PitchForkPoint : MonoBehaviour
    {
        private PitchFork pitchFork;

        private Vector3 lastClosestPoint;
        private float forkedDistance;

        private CultivationField lastField;

        private void Awake()
        {
            pitchFork = GetComponentInParent<PitchFork>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Soil")
            {
                if(other.TryGetComponent(out CultivationField cultivationField))
                {
                    lastClosestPoint = other.ClosestPoint(transform.position);
                    cultivationField.PlowGround(10);

                    lastField = cultivationField;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Soil")
            {
                if(lastField.gameObject != other.gameObject)
                {
                    lastField = other.GetComponent<CultivationField>();
                    return;
                }

                Vector3 closestPoint = other.ClosestPoint(transform.position);

                forkedDistance += Vector3.Distance(closestPoint, lastClosestPoint) * 10f;
                lastClosestPoint = other.ClosestPoint(transform.position);

                if(forkedDistance > 1)
                {
                    lastField.PlowGround((int)forkedDistance);
                    forkedDistance = 0f;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            lastField = null;
            forkedDistance = 0f;
        }
    }
}

