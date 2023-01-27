using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private CursorManager cursorManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
        if (inventory.activeSelf)
        {
            cursorManager.cursorLock = false;
            Time.timeScale = 0.25f;
        }
        else
        {
            cursorManager.cursorLock = true;
            Time.timeScale = 1;
        }
    }
}
