using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertCurrency : MonoBehaviour
{
    //TO-DO: create a button/slider system for changing souls into currency
    //and changing the values of abilities with said currency
    [SerializeField] private InventoryData inventory;
    [SerializeField] private float currencyValue = 0.25f;

    public void TransferSoul()
    {
        if (inventory.soulAmount > 0)
        {
            inventory.currency += (currencyValue * inventory.soulAmount);
            inventory.soulAmount = 0;
        }
        else
        {
            // Make some txt appear
        }
    }
}
