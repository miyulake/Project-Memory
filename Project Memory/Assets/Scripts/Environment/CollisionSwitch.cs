using UnityEngine;

public class CollisionSwitch : MonoBehaviour
{
    [SerializeField] private GameObject createdObject;
    [SerializeField] private GameObject destroyedObject;

    private void Awake()
    {
        createdObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            createdObject.SetActive(true);
            destroyedObject.SetActive(false);
        }
    }
}