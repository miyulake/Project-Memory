using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private SetActive[] objects;

    private void Awake()
    {
        foreach (var gameObject in objects)
        {
            gameObject.Deactivate();

            StartCoroutine(gameObject.ActivateObject());
        }
    }
}

[System.Serializable]
public class SetActive
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private float waitTime;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator ActivateObject()
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(true);
    }
}
