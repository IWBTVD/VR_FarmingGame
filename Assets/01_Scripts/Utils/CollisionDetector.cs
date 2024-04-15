using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public UnityEvent<Collision> OnEnter = new();
    public UnityEvent<Collision> OnStay = new();
    public UnityEvent<Collision> OnExit = new();

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Pickaxe Collided!");
        OnEnter.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnStay.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnExit.Invoke(collision);
    }
}
