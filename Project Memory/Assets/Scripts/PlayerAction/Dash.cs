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
    [SerializeField] private KeyCode dashInput;

    [Header("Input Values")]
    [SerializeField] private float inputTime;
    private float inputTimer;

    [Header("UI")]
    [SerializeField] private GameObject dashUI;
    [SerializeField] private RawImage dashIcon;
    [SerializeField] private Texture[] textureList;
    [SerializeField] private RawImage dashAura;
    [SerializeField] private float degrees;

    private Vector3 rotationEuler;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip chargingSound;
    [SerializeField] private AudioClip readySound;
    [SerializeField] private float audioVolume;

    private bool playOnce = true;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        if (dashSetting.canDash)
        {
            dashUI.SetActive(true);
        }
        else
        {
            dashUI.SetActive(false);
        }
    }

    private void Update()
    {
        dashAura.transform.rotation = Quaternion.Euler(rotationEuler);

        if (dashSetting.canDash)
        {
            if (Input.GetKey(dashInput) && inputTimer < inputTime)
            {
                var percentage = Mathf.Clamp01(inputTimer / inputTime);
                var currentIndex = Mathf.RoundToInt((textureList.Length - 1) * percentage);

                inputTimer += Time.deltaTime;

                dashIcon.color = new Color(1, 1, 1, 0.25f + percentage);
                dashAura.color = new Color(1, 0, 0, 0 + percentage);
                ChangeSprite(currentIndex);
            }

            if (Input.GetKey(dashInput))
            {
                rotationEuler += Vector3.forward * degrees * Time.deltaTime;
            }

            if (Input.GetKeyDown(dashInput))
            {
                audioManager.PlayOneShot(chargingSound, audioVolume);
                playOnce = true;
            }

            if (Input.GetKeyUp(dashInput) && inputTimer < inputTime)
            {
                inputTimer = 0;

                dashIcon.color = new Color(1, 1, 1, 0.25f);
                dashAura.color = new Color(1, 0, 0, 0);
                ChangeSprite(0);

                rotationEuler += Vector3.forward * 0 * Time.deltaTime;

                audioManager.Stop();
            }

            if (playOnce && inputTimer >= inputTime)
            {
                audioManager.Stop();
                audioManager.PlayOneShot(readySound, audioVolume);

                playOnce = false;
            }

            if (Input.GetKeyUp(dashInput) && inputTimer >= inputTime)
            {
                inputTimer = 0;

                dashIcon.color = new Color(1, 1, 1, 0.25f);
                dashAura.color = new Color(1, 0, 0, 0);
                ChangeSprite(0);

                rotationEuler += Vector3.forward * 0 * Time.deltaTime;

                StartCoroutine(DashAction());

                audioManager.PlayOneShot(dashSound, audioVolume);
            }
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

        //dashScreen.SetActive(false);
    }

    private void ChangeSprite(int index)
    {
        dashIcon.texture = textureList[index];
    }
}