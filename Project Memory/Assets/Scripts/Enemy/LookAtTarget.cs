using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float chaseSpeed = 1;

    [SerializeField] private CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //transform.LookAt(target);

        var offsetToTarget = target.position - transform.position;
        offsetToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(offsetToTarget);

        //transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 1.5f, 0), chaseSpeed * Time.deltaTime);

        var speed = 1 * Time.deltaTime;
        var movement = transform.forward * chaseSpeed * Time.deltaTime;
        movement.y = 0;
        cc.Move(movement);

        //Vector3 eulerRotation = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(0, eulerRotation.y + 180, eulerRotation.z);
    }
}