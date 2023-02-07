using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UseOrb : MonoBehaviour
{
    [SerializeField] private InventoryData inventory;
    [SerializeField] private UnityEvent insertOrbEvent;
    [SerializeField] private UnityEvent removeOrbEvent;
    [SerializeField] private GameObject pillarOrb;
    [SerializeField] private GameObject pillarText;
    [SerializeField] private TextMeshProUGUI textMesh;

    private bool hasOrb;

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textMesh.text = "Press " + '"' + "E" + '"' + " to insert orb";
            pillarText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && inventory.orbAmount > 0 && !hasOrb)
            {
                InsertOrb();
            }
            else if (Input.GetKeyDown(KeyCode.E) && hasOrb)
            {
                RemoveOrb();
            }
        }
        if (col.CompareTag("Player") && hasOrb)
        {
            textMesh.text = "Press " + '"' + "E" + '"' + " to remove orb";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player")) pillarText.SetActive(false);
    }

    private void InsertOrb()
    {
        insertOrbEvent.Invoke();
        pillarOrb.SetActive(true);
        inventory.orbAmount -= 1;
        hasOrb = true;
    }
    private void RemoveOrb()
    {
        removeOrbEvent.Invoke();
        pillarOrb.SetActive(false);
        inventory.orbAmount += 1;
        hasOrb = false;
    }
}
