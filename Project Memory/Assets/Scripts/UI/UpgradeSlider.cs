using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSlider : MonoBehaviour
{
    [SerializeField] private InventoryData inventory;

    [SerializeField] private Slider upgradeSlider;
    [SerializeField] private TextMeshProUGUI minValueMesh;
    [SerializeField] private TextMeshProUGUI currentValueMesh;
    [SerializeField] private TextMeshProUGUI maxValueMesh;

    [SerializeField] private float minFloatValue;
    [SerializeField] private float maxFloatValue;
    [SerializeField] private float minIntValue;
    [SerializeField] private float maxIntValue;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
