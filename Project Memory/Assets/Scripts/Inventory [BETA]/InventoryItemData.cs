using UnityEngine;

//[CreateAssetMenu(fileName = "Inventory Item Data", menuName = "ScriptableObjects/Inventory Item Datas", order = 1)]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}
