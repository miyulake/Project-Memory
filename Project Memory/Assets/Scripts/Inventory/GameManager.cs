using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current { get; private set; }

    public GameObject player;

    public InventorySystem inventorySystem;

    public GameManager()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
    }
}
