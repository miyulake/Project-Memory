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
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerLocation.position = teleportLocations[4].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerLocation.position = teleportLocations[5].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playerLocation.position = teleportLocations[6].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            playerLocation.position = teleportLocations[7].position;
        }
    }
}
