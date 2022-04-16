using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float degrees;

    private Vector3 rotationEuler;

    private void Update()
    {
        rotationEuler += Vector3.forward * degrees * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
