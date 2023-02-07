using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractSave : MonoBehaviour
{
    [SerializeField] private JsonReadWriteSystem jsonReadWriteSystem;
    [SerializeField] private GameObject saveText;
    [SerializeField] private TextMeshProUGUI textMesh;

    private bool hasSaved;

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textMesh.text = "Press " + '"' + "E" + '"' + " to save";
            saveText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !hasSaved)
            {
                jsonReadWriteSystem.SaveToJson();
                hasSaved = true;
            }
        }
        if (col.CompareTag("Player") && hasSaved)
        {
            textMesh.text = "Saved game!";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            saveText.SetActive(false);
            hasSaved = false;
        }
    }
}
