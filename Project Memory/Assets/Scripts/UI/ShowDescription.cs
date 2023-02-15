using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDescription : MonoBehaviour
{
    //TO-DO show an information box with item descriptions
    //at the mouse origin point when hovering over said item
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private RectTransform descriptionObj;

    private void Update()
    {
        descriptionObj.anchoredPosition = Input.mousePosition;
    }

    public void InsertDescription(string descriptionText)
    {
        textMesh.text = descriptionText;
    }
}
