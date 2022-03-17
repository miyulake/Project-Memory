using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    [SerializeField] private float radius;

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, GameManager.current.player.transform.position);
        bool keyPressed = Input.GetKeyDown(KeyCode.E);
        bool inRange = playerDistance <= radius;

        if (keyPressed && inRange)
        {
            PickupItem();
        }
    }

    public void PickupItem()
    {
        GameManager.current.inventorySystem.Add(referenceItem);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
