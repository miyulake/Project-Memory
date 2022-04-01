using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFog : MonoBehaviour
{
    [Header("Fog Values")]
    [SerializeField] private bool fogSwitch;
    [SerializeField] private Color fogColor;
    [SerializeField] private float fogDensity;

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            RenderSettings.fog = fogSwitch;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }
}
