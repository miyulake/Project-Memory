using System.Collections;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (target != null)
        {
            var offsetToTarget = target.position - transform.position;
            offsetToTarget.y = 0;
            transform.rotation = Quaternion.LookRotation(offsetToTarget);
        }
    }
}