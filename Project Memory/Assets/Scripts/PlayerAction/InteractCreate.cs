using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCreate : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject item;

    private void Awake()
    {
        interactText.SetActive(false);

        if (item != null)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter()
    {
        interactText.SetActive(true);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            interactText.SetActive(false);

            if (item != null)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            interactText.SetActive(false);

            if (item != null)
            {
                item.SetActive(false);
            }
        }
    }
}
