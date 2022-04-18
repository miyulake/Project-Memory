using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [Header("Text Values")]
    [SerializeField] [TextArea] private string insertedText;
    [SerializeField] private float textSpeed = 0.05f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI displayedText;
    private int currentDisplayingText = 0;

    [Header("Audio Elements")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip scrollSound;
    [SerializeField] private float audioVolume = 1;
    [SerializeField] private float minimumPitch = 0.9f;
    [SerializeField] private float maximumPitch = 1f;

    [Space]
    [SerializeField] private bool autoActive;
    [SerializeField] private bool useName;
    [SerializeField] private NameData nameData;

    private void Update()
    {
        if (autoActive)
        {
            StartCoroutine(AnimateText());
            autoActive = false;
        }
    }

    public void ActivateText()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        if (!useName)
        {
            for (int i = 0; i < insertedText.Length + 1; i++)
            {
                displayedText.text = AddCharacter(insertedText, i);
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            var nameText = $"{nameData.insertedTextBefore}{nameData.gameSettings.playerName}{nameData.insertedTextAfter}";

            for (int i = 0; i < nameText.Length + 1; i++)
            {
                displayedText.text = AddCharacter(nameText, i);
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    private string AddCharacter(string displayText, int index)
    {
        var result = displayText.Substring(0, index);

        audioManager.pitch = Random.Range(minimumPitch, maximumPitch);
        audioManager.PlayOneShot(scrollSound, audioVolume);

        return result;
    }
}