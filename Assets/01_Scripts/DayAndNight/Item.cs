using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update

    public enum ItemType
    {
        carrot,
        apple,
        orange,
        banana,
    }

    public ItemType itemType;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public new string GetType()
    {
        // Debug.Log("Item Type : " + itemType.name);
        return itemType.ToString();
    }
}
