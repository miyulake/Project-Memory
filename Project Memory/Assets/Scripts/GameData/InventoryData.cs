using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "ScriptableObjects/Inventory Data", order = 1)]
public class InventoryData : ScriptableObject
{
    public enum Abilities
    {
        none,
        dash,
        teleport,
        platform,
        transparent,
        invincible,
        lsd
    }
    public enum Items
    {
        none,
        torch,
        sword,
        orb,
        soul
    }

    [Header("Currently Equipped")]
    public Abilities ability;
    public Items item;

    [Header("Inventory Amounts")]
    public int soulAmount, orbAmount;

    [Header("Currency")]
    public float currency;

    [Header("Dash Values")]
    [Range(3, 15)] public float dashSpeed = 5;
    [Range(0.1f, 3)] public float dashInputDuration = 1;

    [Header("Teleport Values")]
    [Range(0, 99)] public int teleportAmount = 3;
    [Range(1, 6)] public float teleportInputDuration = 6;

    [Header("Platform Values")]
    [Range(3, 10)] public float platformDuration = 5;
    [Range(2, 6)] public float platformInputDuration = 4;
    public bool canRotate;

    // Can be unlocked by the player for spending 100 souls!
    [Header("DEBUG")]
    public bool canUseDebug;
    public float dashDuration = 0.25f;
    public float platformSpeed = 1.5f;

}