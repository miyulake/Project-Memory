using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMoveObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveAudio;
    [SerializeField] private AudioClip stopAudio;
    [SerializeField] private Vector3 moveOffset;
    [Space]
    [SerializeField] private float time;
    [SerializeField] private bool stop = true;


    private void Update()
    {
        if (!stop)
        {
            transform.position += moveOffset * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && stop)
        {
            StartMove();
        }
    }

    private void StartMove()
    {
        stop = false;

        audioSource.PlayOneShot(moveAudio);

        Invoke("StopObject", time);
    }

    private void StopObject()
    {
        stop = true;

        audioSource.PlayOneShot(stopAudio);
    }
}
