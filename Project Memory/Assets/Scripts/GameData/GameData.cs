using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Player Position
    public Vector3 playerPos;

    // Loot List
    public List<GameObject> loot;

    // Inventory Amounts
    public int soulAmount;
    public int orbAmount;
    public float currency;

    // Ability Data Checks
    public bool hasDash;
    public bool hasTeleport;
    public bool hasPlatform;
    public bool hasTransparent;
    public bool hasInvincible;
    public bool hasLSD;

    // Ability Data Values

    // Inventory Data Checks
    public bool hasTorch;
    public bool hasSword;
}
