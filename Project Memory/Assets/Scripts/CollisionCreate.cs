using UnityEngine;

public class CollisionCreate : MonoBehaviour
{
    [SerializeField] private GameObject createdObject;

    private void Awake()
    {
        createdObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            createdObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}