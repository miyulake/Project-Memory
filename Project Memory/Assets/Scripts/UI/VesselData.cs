using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VesselData : MonoBehaviour
{
    [SerializeField] public GameSettings gameSettings;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] [TextArea] public string insertedTextBefore;
    [SerializeField] [TextArea] public string insertedTextAfter;
    [SerializeField] private TextMeshProUGUI displayText;

    public void StoreVessel()
    {
        if (inputField.text.Length > 0)
        {
            gameSettings.playerVessel = inputField.text;
        }
        if (inputField.text.Length <= 0)
        {
            gameSettings.playerVessel = "Existence";
        }
    }

    public void ShowVessel()
    {
        displayText.text = insertedTextBefore + gameSettings.playerVessel + insertedTextAfter;
    }
}
