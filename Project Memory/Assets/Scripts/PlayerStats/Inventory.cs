using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "ScriptableObjects/Inventory Data", order = 1)]
public class Inventory : ScriptableObject
{
    public enum Abilities
    {
        dash,
        teleport,
        platform,
        transparent,
        lsd,
    }

    public Abilities ability = Abilities.dash;

    public int soulAmount, orbAmount;

    public int dashLevel, platformLevel, transparentLevel;
}
