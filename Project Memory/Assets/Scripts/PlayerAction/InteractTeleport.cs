using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTeleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teleportTarget;

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = teleportTarget.transform.position;
        }
    }
}
