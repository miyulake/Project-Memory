using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "ScriptableObjects/Game Settings", order = 2)]
public class GameSettings : ScriptableObject
{
    [Header("Player Settings")]
    [Range(40, 120)] public float playerFOV = 70;
    public string playerName;
    public string playerVessel;
    public bool canDash;

    [Header("Game Settings")]
    public bool postEnabled;
}
