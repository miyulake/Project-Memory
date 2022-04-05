using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FOVText : MonoBehaviour
{
    [Space]
    [SerializeField] private TextMeshProUGUI fieldOfViewText;
    [SerializeField] private GameSettings playerSettings;

    private void Update()
    {
        fieldOfViewText.text = "" + Mathf.FloorToInt(playerSettings.playerFOV);
    }
}
