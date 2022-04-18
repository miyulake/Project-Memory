using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayData : MonoBehaviour
{
    [SerializeField] private NameData   nameData;
    [SerializeField] private VesselData vesselData;

    [SerializeField] private bool showName;
    [SerializeField] private bool showVessel;


    private void Update()
    {
        if (showName)   nameData.ShowName();
        if (showVessel) vesselData.ShowVessel();
    }
}
