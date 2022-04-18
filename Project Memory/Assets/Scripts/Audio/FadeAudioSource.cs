using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool autoFade;

    [SerializeField] private bool fadeOut = true;

    private void Update()
    {
        if (autoFade) FadeAudio();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            FadeAudio();
        }
    }

    public void FadeAudio()
    {
        if (fadeOut) StartCoroutine(StartFade(audioSource, fadeDuration, 0));
        else StartCoroutine(StartFade(audioSource, fadeDuration, 1));
    }

    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}