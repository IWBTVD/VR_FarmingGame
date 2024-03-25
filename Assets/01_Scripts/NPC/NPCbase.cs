using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class NPCbase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Interact()
        {
            Debug.Log("Interacting with base class");
        }
    }

}
