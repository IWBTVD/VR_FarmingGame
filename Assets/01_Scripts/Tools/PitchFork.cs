using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class PitchFork : MonoBehaviour
    {
        private Autohand.Grabbable grabbable;
        private PitchForkPoint pitchForkPoint;

        private void Awake()
        {
            grabbable = GetComponent<Autohand.Grabbable>();

        }


    }

}
