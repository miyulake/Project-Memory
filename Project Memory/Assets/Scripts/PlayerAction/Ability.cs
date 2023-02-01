using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Space]
    [SerializeField] private CharacterMovement moveScript;
    [SerializeField] private GameSettings abilitySetting;
    [SerializeField] private Inventory inventory;

    [Header("Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform headPivot;
    [SerializeField] private GameObject platform;

    [Header("Ability Values")]
    public float abilitySpeed;
    public float abilityDuration;
    [SerializeField] private KeyCode abilityInput;

    [Header("Input Values")]
    public float inputDuration;
    private float inputTimer;

    [Header("UI")]
    [SerializeField] private GameObject abilityUI;
    [SerializeField] private RawImage abilityIcon;
    [SerializeField] private Texture[] textureList;
    [SerializeField] private RawImage abilityAura;
    [SerializeField] private float degrees;

    [Header("Textures")]
    [SerializeField] private Texture[] dashTextures;
    [SerializeField] private Texture[] teleportTextures;
    [SerializeField] private Texture[] platformTextures;

    private Vector3 rotationEuler;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip dashSound, teleportSound, platformSound;
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
        if (abilitySetting.canAbility)
        {
            abilityUI.SetActive(true);
            abilityAura.color = new Color(1, 0, 0, 0);
        }
        else
        {
            abilityUI.SetActive(false);
        }
    }

    private void Update()
    {
        SetValues();

        abilityAura.transform.rotation = Quaternion.Euler(rotationEuler);

        if (abilitySetting.canAbility)
        {
            if (Input.GetKey(abilityInput) && inputTimer < inputDuration)
            {
                var percentage = Mathf.Clamp01(inputTimer / inputDuration);
                var currentIndex = Mathf.RoundToInt((textureList.Length - 1) * percentage);

                inputTimer += Time.deltaTime;

                abilityIcon.color = new Color(1, 1, 1, 0.25f + percentage);
                abilityAura.color = new Color(1, 0, 0, 0 + percentage);
                ChangeSprite(currentIndex);
            }

            if (Input.GetKey(abilityInput))
            {
                rotationEuler += Vector3.forward * degrees * Time.deltaTime;
            }

            if (Input.GetKeyDown(abilityInput))
            {
                audioManager.PlayOneShot(chargingSound, audioVolume);
                playOnce = true;
            }

            if (Input.GetKeyUp(abilityInput) && inputTimer < inputDuration)
            {
                inputTimer = 0;

                abilityIcon.color = new Color(1, 1, 1, 0.25f);
                abilityAura.color = new Color(1, 0, 0, 0);
                ChangeSprite(0);

                rotationEuler += Vector3.forward * 0 * Time.deltaTime;

                audioManager.Stop();
            }

            if (playOnce && inputTimer >= inputDuration)
            {
                audioManager.Stop();
                audioManager.PlayOneShot(readySound, audioVolume);

                playOnce = false;
            }

            if (Input.GetKeyUp(abilityInput) && inputTimer >= inputDuration)
            {
                inputTimer = 0;

                abilityIcon.color = new Color(1, 1, 1, 0.25f);
                abilityAura.color = new Color(1, 0, 0, 0);
                ChangeSprite(0);

                rotationEuler += Vector3.forward * 0 * Time.deltaTime;

                // TO-DO: make it so that the script checks what ability is currently in use
                // and create corresponding actions!
                if (inventory.ability == Inventory.Abilities.dash)
                {
                    StartCoroutine(DashAction());
                    audioManager.PlayOneShot(dashSound, audioVolume);
                }
                if (inventory.ability == Inventory.Abilities.teleport)
                {
                    Teleport();
                    audioManager.PlayOneShot(teleportSound, audioVolume);
                }
                if (inventory.ability == Inventory.Abilities.platform)
                {
                    SpawnPlatform();
                    audioManager.PlayOneShot(platformSound, audioVolume);
                }
            }
        }
    }

    // Dash ability
    private IEnumerator DashAction()
    {
        float startTime = Time.time;

        while (Time.time < startTime + abilityDuration)
        {
            moveScript.characterController.Move(moveScript.HorizontalVelocity * abilitySpeed * Time.deltaTime);

            yield return null;
        }
    }

    // Teleport ability
    private void Teleport()
    {
        if (inventory.teleportAmount > 0)
        {
            //TO-DO: teleport ability to get players out of dungeons!
        }
    }

    // Platform ability
    private void SpawnPlatform()
    {
        if (inventory.canRotate)
        {
            Instantiate(platform, player.transform.position + (player.transform.forward * 1.5f) + (player.transform.up * -0.25f), headPivot.rotation);
        }
        else
        {
            Instantiate(platform, player.transform.position + (player.transform.forward * 1.5f) + (player.transform.up * -0.25f), player.transform.rotation);
        }
    }

    // Sets the ability values for each ability
    private void SetValues()
    {
        if (inventory.ability == Inventory.Abilities.dash)
        {
            textureList = dashTextures;
            abilitySpeed = inventory.dashSpeed;
            abilityDuration = inventory.dashDuration;
            inputDuration = inventory.dashInputDuration;
        }
        else if (inventory.ability == Inventory.Abilities.teleport)
        {
            textureList = teleportTextures;
            //abilitySpeed = inventory.dashSpeed;
            //abilityDuration = inventory.dashDuration;
            inputDuration = inventory.teleportInputDuration;
        }
        else if (inventory.ability == Inventory.Abilities.platform)
        {
            textureList = platformTextures;
            abilitySpeed = inventory.platformSpeed;
            abilityDuration = inventory.platformDuration;
            inputDuration = inventory.platformInputDuration;
        }
    }

    // Scrolls through the sprites of the abilities icon
    private void ChangeSprite(int index)
    {
        abilityIcon.texture = textureList[index];
    }
}