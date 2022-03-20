using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private float speed = 1f;

    void Update()
    {
        var step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, step);

        if (gameObject.transform.position == target.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
