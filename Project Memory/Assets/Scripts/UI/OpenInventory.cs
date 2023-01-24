using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
        if (inventory.activeSelf)
        {
            Time.timeScale = 0.25f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
