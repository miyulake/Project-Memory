using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryData inventory;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private TextMeshProUGUI soulAmount, orbAmount, currencyAmount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AssignValue();
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (inventoryUI.activeSelf)
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

    private void AssignValue()
    {
        soulAmount.text = inventory.soulAmount.ToString() + "x";
        orbAmount.text = inventory.orbAmount.ToString() + "x";
        currencyAmount.text = inventory.currency.ToString() + "x";
    }
}
