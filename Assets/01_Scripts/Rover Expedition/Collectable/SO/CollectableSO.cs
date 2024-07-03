using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectables/Collectable")]
public class CollectableSO : ScriptableObject
{
    public string Name;
    public string Description;
    public int weight;
}
