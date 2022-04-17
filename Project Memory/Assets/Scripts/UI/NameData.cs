using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameData : MonoBehaviour
{
    [SerializeField] public GameSettings gameSettings;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] [TextArea] public string[] insertedTextBefore;
    [SerializeField] [TextArea] public string[] insertedTextAfter;
    [SerializeField] private TextMeshProUGUI displayText;

    private void Start()
    {
        gameSettings.name = "Challenger";
    }

    public void StoreName()
    {
        if (inputField.text.Length > 0)
        {
            gameSettings.playerName = inputField.text;
        }
        if (inputField.text.Length <= 0)
        {
            gameSettings.playerName = "Challenger";
        }
    }

    public void ShowName()
    {
        displayText.text = insertedTextBefore + " " + gameSettings.playerName + " " + insertedTextAfter;
    }
}
