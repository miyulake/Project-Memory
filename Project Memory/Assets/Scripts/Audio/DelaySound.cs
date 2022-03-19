using UnityEngine;
using System.Collections;

public class DelaySound : MonoBehaviour
{

    [SerializeField] private AudioClip sound;

    [SerializeField] private int waitTime = 10;

    [SerializeField] private bool keepPlaying = true;

    private void Start()
    {
        StartCoroutine(SoundOut());
    }

    IEnumerator SoundOut()
    {
        while (keepPlaying)
        {
            yield return new WaitForSeconds(waitTime);

            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
}