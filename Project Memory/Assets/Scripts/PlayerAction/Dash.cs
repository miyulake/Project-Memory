using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [Space]
    [SerializeField] private CharacterMovement moveScript;
    [SerializeField] private GameSettings dashSetting;

    [Header("Dash Values")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private KeyCode dashInput;

    [Header("UI")]
    [SerializeField] private RawImage dashScreen;
    [SerializeField] private float screenFadeSpeed;
    [SerializeField] private Color transparent;
    [SerializeField] private Color opaque;

    [SerializeField] private RawImage dashIcon;
    [SerializeField] private Texture canDashIcon;
    [SerializeField] private Texture cannotDashIcon;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private float audioVolume;

    //private bool canDash;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        dashSetting.canDash = true;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(dashInput) || Input.GetKeyUp(dashInput)) && dashSetting.canDash)
        {
            StartCoroutine(DashAction());
            StartCoroutine(DashCooldown());

            dashScreen.color = Color.Lerp(transparent, opaque, screenFadeSpeed);
            dashIcon.color = new Color(1, 1, 1, 0.5f);
            dashIcon.texture = cannotDashIcon;

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

        dashScreen.color = Color.Lerp(opaque, transparent, screenFadeSpeed);
    }

    IEnumerator DashCooldown()
    {
        dashSetting.canDash = false;

        yield return new WaitForSeconds(dashCooldown);

        dashIcon.color = new Color(1, 1, 1, 1);
        dashIcon.texture = canDashIcon;

        dashSetting.canDash = true;
    }
}
