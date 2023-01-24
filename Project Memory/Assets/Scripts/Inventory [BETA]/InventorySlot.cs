using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image Icon;

    [SerializeField] private TextMeshProUGUI Label;

    [SerializeField] private GameObject stackObj;

    [SerializeField] private TextMeshProUGUI stackLabel;

    public void Set(InventoryItem item)
    {
        Icon.sprite = item.data.icon;
        Label.text = item.data.displayName;
        if(item.stackSize <= 1)
        {
            stackObj.SetActive(false);
            return;
        }

        stackLabel.text = item.stackSize.ToString();
    }
}
