using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyData : MonoBehaviour
{
    [SerializeField] public GameSettings gameSettings;

    public void Start()
    {
        gameSettings.difficulty = GameSettings.PlayerDifficulty.normal;
    }

    public void SetDifficulty(int difficultyIndex)
    {
        gameSettings.difficulty = (GameSettings.PlayerDifficulty)difficultyIndex;
    }
}
