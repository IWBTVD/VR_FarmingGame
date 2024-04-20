using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Jun.Ground.Crops;

public class SeedBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected Grabbable grabbable;
    
    protected SeedSacBase seedSac;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();

        grabbable.onGrab.AddListener(GrabbedFirst);
    }

    public void GrabbedFirst(Hand hand, Grabbable grabbable)
    {
        if(seedSac != null)
        {
            DecoupleWithSeedSac();
        }
    }

    public void CreatedBySeedSac(SeedSacBase sac)
    {
        seedSac = sac;
        rb.isKinematic = true;
    }

    /// <summary>
    /// 주머니와 연결 끊기
    /// </summary>
    public void DecoupleWithSeedSac()
    {
        seedSac = null;
        rb.isKinematic = false;
    }
}
