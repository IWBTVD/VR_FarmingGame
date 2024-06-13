using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("collided " + collision.gameObject.name + collision.relativeVelocity.magnitude);
    }
}
