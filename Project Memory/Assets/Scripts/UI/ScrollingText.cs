using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [Header("Text Values")]
    [SerializeField] [TextArea] private string[] insertedText;
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
        for (int i = 0; i < insertedText[currentDisplayingText].Length + 1; i++)
        {
            displayedText.text = insertedText[currentDisplayingText].Substring(0, i);
            audioManager.pitch = Random.Range(minimumPitch, maximumPitch);
            audioManager.PlayOneShot(scrollSound, audioVolume);
            yield return new WaitForSeconds(textSpeed);
        }
    }


}
