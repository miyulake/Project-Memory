using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager current { get; private set; }

    public GameObject player;

    public InventorySystem inventorySystem;

    [SerializeField] private GameSettings playerSettings;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject urpVolume;

    public void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
    }
    private void Start()
    {
        playerCamera.fieldOfView = playerSettings.playerFOV;

        urpVolume.SetActive(playerSettings.postEnabled);
    }
}
