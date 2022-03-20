using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject environmentObject;
    [SerializeField] private GameObject inventoryObject;

    [Header("Text")]
    [SerializeField] private GameObject interactedText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip interactAudio;
    [SerializeField] private float audioVolume = 1;

    private void Awake()
    {
        inventoryObject.SetActive(false);
        interactedText.SetActive(false);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            environmentObject.SetActive(false);
            inventoryObject.SetActive(true);
            interactedText.SetActive(true);

            audioManager.PlayOneShot(interactAudio, audioVolume);
        }
    }
}
