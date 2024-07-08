using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePartSpawner : MonoBehaviour
{
    public VehiclePartList Parts;   //부품 리스트
    private List<GameObject> spawnedParts = new List<GameObject>();

    void Start()
    {
        SpawnParts();    //게임 시작 시 부품들 소환
    }


    private void SpawnParts()     //부품 소환     
    {
        foreach (var vehicleStructure in Parts.parts)
        {
            GameObject obj = Instantiate(vehicleStructure.prefab);
            obj.SetActive(false);      // 비활성화
            spawnedParts.Add(obj);

        }
    }


    public void ActiveParts()          //구매 등의 연유로 비활성화되어있는 부품 활성화
    {
        foreach(var obj in spawnedParts)
        {
            obj.SetActive(true);
        }
    }
}
