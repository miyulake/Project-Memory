using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform[] teleportLocations;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerLocation.position = teleportLocations[0].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerLocation.position = teleportLocations[1].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerLocation.position = teleportLocations[2].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerLocation.position = teleportLocations[3].position;
        }
    }
}
