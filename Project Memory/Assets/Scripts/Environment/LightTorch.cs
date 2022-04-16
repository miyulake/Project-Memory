using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : MonoBehaviour
{
    [SerializeField] private GameObject fire;

    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip igniteSound;
    [SerializeField] private float audioVolume = 1;

    [SerializeField] private bool isLit = true;

    private void Start()
    {
        if (!isLit)
        {
            fire.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Torch") && isLit == false)
        {
            fire.SetActive(true);
            audioManager.PlayOneShot(igniteSound, audioVolume);
            isLit = true;
        }
    }
}
