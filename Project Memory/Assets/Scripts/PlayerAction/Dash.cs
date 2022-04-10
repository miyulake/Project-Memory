using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Space]
    [SerializeField] private CharacterMovement moveScript;

    [Header("Dash Values")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private KeyCode dashInput;

    private bool canDash;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private float audioVolume;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        canDash = true;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(dashInput) || Input.GetKeyUp(dashInput)) && canDash)
        {
            StartCoroutine(DashAction());
            StartCoroutine(DashCooldown());
            audioManager.PlayOneShot(dashSound, audioVolume);
        }
    }

    IEnumerator DashAction()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.characterController.Move(moveScript.HorizontalVelocity * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
