using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
using Oculus.VR;

public class CollectionEquipment : MonoBehaviour
{
    public GatheringToolParts leftHandController;       //왼손 컨트롤러 할당
    public GatheringToolParts rightHandController;      //오른손 컨트롤러 할당

    private bool isWorn = false;

    public void WearEquipment()
    {
        isWorn = true;
        UpdateHandControllers();
    }

    public void RemoveEquipment()
    {
        isWorn = false;
        UpdateHandControllers();
    }

    private void UpdateHandControllers()
    {
        if (leftHandController != null)
        {
            leftHandController.SetCollectionEquipmentOn(isWorn);
        }

        if (rightHandController != null)
        {
            rightHandController.SetCollectionEquipmentOn(isWorn);
        }
    }
}
