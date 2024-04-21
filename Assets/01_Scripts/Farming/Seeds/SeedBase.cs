using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Jun.Ground.Crops;
using Gun;

public class SeedBase : MonoBehaviour
{
    [SerializeField] protected PlantBase _plantPrefab;

    protected Rigidbody rb;
    protected Grabbable grabbable;
    
    protected SeedSacBase seedSac;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();

        grabbable.onGrab.AddListener(OnFirstGrab);
    }

    public void OnFirstGrab(Hand hand, Grabbable grabbable)
    {
        if(seedSac != null)
        {
            DecoupleWithSeedSac();

            grabbable.onGrab.RemoveListener(OnFirstGrab);
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
        grabbable.originalParent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnterTrigger");
        if (other.TryGetComponent(out CropPoint cropPoint))
        {
            cropPoint.PlantCrop(_plantPrefab);
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
