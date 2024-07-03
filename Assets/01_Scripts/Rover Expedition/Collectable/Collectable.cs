using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Collectable : MonoBehaviour
{
    public CollectableSO collectableSO;
    public bool isCollectable; // true: collectable, false: not collectable only investigatable
    public Canvas infoCanvas; // Show name and description

    public void TryCollect()
    {
        Debug.Log($"Trying to collect {collectableSO.Name}");

        if (isCollectable)
        {
            OnCollected();
        }
        else
        {
            OnInvestigate();
        }
    }

    public void OnCollected()
    {
        Debug.Log($"{collectableSO.Name} has been collected!");
    }

    public void OnInvestigate()
    {
        Debug.Log($"{collectableSO.Name} has been investigated!");
        infoCanvas.gameObject.SetActive(true);
    }


}
