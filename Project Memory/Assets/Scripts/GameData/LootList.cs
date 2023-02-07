using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot List", menuName = "ScriptableObjects/Loot List", order = 2)]
public class LootList : ScriptableObject
{
    [Header("List of items picked up (This is debugging only!)")]
    public List<GameObject> lootedObjects;
}
