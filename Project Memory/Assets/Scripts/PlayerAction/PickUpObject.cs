using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI textMesh;
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
        if (inventoryObject != null) inventoryObject.SetActive(false);
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

            textMesh.text = "Picked Up " + '"' + itemType.ToString() + '"';
            StartCoroutine(DisplayText());
            environmentObject.SetActive(false);
            if(inventoryObject != null) inventoryObject.SetActive(true);

            audioManager.PlayOneShot(interactAudio, audioVolume);
        }
    }

    private void AssignItem()
    {
        if (itemType == Inventory.Items.soul)
        {
            inventory.soulAmount += 1;
        }
        if (itemType == Inventory.Items.orb)
        {
            inventory.orbAmount += 1;
        }
    }

    private IEnumerator DisplayText()
    {
        pickedUpText.SetActive(true);

        yield return new WaitForSeconds(2);

        pickedUpText.SetActive(false);
    }
}
