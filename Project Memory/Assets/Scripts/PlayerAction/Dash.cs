using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [Space]
    [SerializeField] private CharacterMovement moveScript;

    [Header("Dash Values")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private KeyCode dashInput;

    [Header("UI")]
    [SerializeField] private GameObject dashScreen;
    [SerializeField] private RawImage dashIcon;
    [SerializeField] private Texture canDashIcon;
    [SerializeField] private Texture cannotDashIcon;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private float audioVolume;

    private bool canDash;

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

            dashScreen.SetActive(true);
            dashIcon.color = new Color(1, 1, 1, 0.5f);
            dashIcon.texture = cannotDashIcon;

            audioManager.PlayOneShot(dashSound, audioVolume);
        }
        else
        {
            dashScreen.SetActive(false);
            dashIcon.color = new Color(1, 1, 1, 1);
            dashIcon.texture = canDashIcon;
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
