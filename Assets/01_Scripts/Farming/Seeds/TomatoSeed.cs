using Autohand;
using Gun;
using Jun.Ground.Crops;
using Pinwheel.Griffin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSeed : SeedBase
{
    [SerializeField] private PlantBase plant;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnterTrigger");
        if(other.TryGetComponent(out CropPoint cropPoint))
        {
            cropPoint.PlantCrop(plant);
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
