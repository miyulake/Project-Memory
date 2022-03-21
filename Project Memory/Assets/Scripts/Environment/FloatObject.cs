using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    [SerializeField] private AnimationCurve path;
    [SerializeField] private float animationLength;

    private float timer;

    private Vector3 startPosition;

    private void Start()
    {
        timer = Random.Range(0, animationLength);

        startPosition = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > animationLength)
        {
            timer = 0;
        }

        transform.position = startPosition + new Vector3(0, path.Evaluate(timer / animationLength));
    }
}