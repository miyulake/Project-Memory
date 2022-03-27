using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float startSpeed = 1;
    [SerializeField] private float endSpeed;
    [SerializeField] private float timeToReachEndspeed = 60;

    [SerializeField] private CharacterController cc;

    private void Start()
    {
        //cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        StartCoroutine(changeValueOverTime(startSpeed, endSpeed, timeToReachEndspeed));

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 0.5f, 0), startSpeed * Time.deltaTime);

        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerRotation.y, eulerRotation.z);

        /*
        var offsetToTarget = target.position - transform.position;
        offsetToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(offsetToTarget);
        
        var speed = 1 * Time.deltaTime;
        var movement = transform.forward * chaseSpeed * Time.deltaTime;
        movement.y = 0;
        cc.Move(movement);
        */

    }

    IEnumerator changeValueOverTime(float fromVal, float toVal, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            startSpeed = val;
            yield return null;
        }
    }
}