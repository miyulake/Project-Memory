using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTeleport : MonoBehaviour
{
    [Space]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teleportTarget;

    [Space]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip teleportAudio;

    [Space]
    [SerializeField] private float SpeedMod;

    [Space]
    [SerializeField] private Image screenImage;
    [SerializeField] private Color teleportColor;
    [SerializeField] private Color targetImageColor;

    [Space]
    [SerializeField] private bool changingColor;

    private void Update()
    {
        if (changingColor)
        {
            screenImage.color = Color.Lerp(screenImage.color, targetImageColor, SpeedMod * Time.deltaTime);
        }

        if (screenImage.color == targetImageColor)
        {
            changingColor = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            screenImage.color = teleportColor;

            audioManager.PlayOneShot(teleportAudio);

            changingColor = true;

            player.transform.position = teleportTarget.transform.position;
        }
    }
}
