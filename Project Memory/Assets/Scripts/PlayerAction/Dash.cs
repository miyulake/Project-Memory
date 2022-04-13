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
    //[SerializeField] private GameObject dashScreen;
    [SerializeField] private RawImage dashIcon;
    [SerializeField] private Texture[] textureList;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private float audioVolume;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(dashInput) && dashSetting.canDash)
        {
            var percentage = Mathf.Clamp01(inputTimer / inputTime);
            var currentIndex = Mathf.RoundToInt((textureList.Length - 1) * percentage);

            inputTimer += Time.deltaTime;

            ChangeSprite(currentIndex);
        }

        if (Input.GetKeyUp(dashInput) && dashSetting.canDash && inputTimer < inputTime) 
        {
            inputTimer = 0;
            ChangeSprite(0);
        }

        if (Input.GetKeyUp(dashInput) && dashSetting.canDash && inputTimer >= inputTime)
        {
            inputTimer = 0;
            ChangeSprite(0);

            StartCoroutine(DashAction());

            //dashScreen.SetActive(true);

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

        //dashScreen.SetActive(false);
    }

    private void ChangeSprite(int index)
    {
        dashIcon.texture = textureList[index];
    }
}