using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour
{
    [Header("InventoryData")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Inventory.Items itemType;

    [Header("Object")]
    [SerializeField] private GameObject environmentObject;
    [SerializeField] private GameObject inventoryObject;

    [Header("UI")]
    [SerializeField] private GameObject pickedUpText;
    [SerializeField] private RawImage pickedUpScreen;
    [SerializeField] private Color pickUpColor;
    [SerializeField] private Color targetImageColor;
    [SerializeField] private float SpeedMod;
    [SerializeField] private bool changingColor;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip interactAudio;
    [SerializeField] private float audioVolume = 1;

    private void Start()
    {
        inventoryObject.SetActive(false);
        pickedUpText.SetActive(false);
    }

    private void Update()
    {
        if (changingColor)
        {
            pickedUpScreen.color = Color.Lerp(pickedUpScreen.color, targetImageColor, SpeedMod * Time.deltaTime);
        }

        if (pickedUpScreen.color == targetImageColor)
        {
            changingColor = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            pickedUpScreen.color = pickUpColor;
            changingColor = true;

            AssignItem();

            pickedUpText.SetActive(true);
            environmentObject.SetActive(false);
            inventoryObject.SetActive(true);

            audioManager.PlayOneShot(interactAudio, audioVolume);
        }
    }

    private void AssignItem()
    {
        if (inventory.item == Inventory.Items.soul)
        {
            inventory.soulAmount += 1;
        }
        if (inventory.item == Inventory.Items.orb)
        {
            inventory.orbAmount += 1;
        }
    }
}
