using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMoveObject : MonoBehaviour
{
    private enum ObjectState
    {
        close,
        open,
        closing,
        opening
    }

    private ObjectState objectState;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveAudio;
    [SerializeField] private AudioClip stopAudio;

    [Space]
    [SerializeField] private Vector3 moveOffset;

    [Space]
    [SerializeField] private float time;
    [SerializeField] private float openTimer;
    [SerializeField] private float waitTime = 5;

    private void Update()
    {
        switch (objectState)
        {
            case ObjectState.opening:
                transform.position += moveOffset * Time.deltaTime;
                break;

            case ObjectState.closing:
                transform.position -= moveOffset * Time.deltaTime;
                break;

            case ObjectState.open:
                openTimer += Time.deltaTime;
                if (openTimer >= waitTime)
                {
                    openTimer = 0;
                    Closing();
                }
                break;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && objectState == ObjectState.close)
        {
            Opening();
        }
    }

    private void Opening()
    {
        objectState = ObjectState.opening;

        audioSource.PlayOneShot(moveAudio);

        Invoke("Open", time);
    }

    private void Open()
    {
        objectState = ObjectState.open;

        audioSource.PlayOneShot(stopAudio);
    }

    private void Closing()
    {
        objectState = ObjectState.closing;

        audioSource.PlayOneShot(moveAudio);

        Invoke("Close", time);
    }

    private void Close()
    {
        objectState = ObjectState.close;

        audioSource.PlayOneShot(stopAudio);
    }
}