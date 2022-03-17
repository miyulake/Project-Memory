using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private Light thunderObject;

    [SerializeField] private AnimationCurve lightningCurve;

    [SerializeField] private float activationTime = 3;
    [SerializeField] private float duration;

    private Coroutine lightningRoutine;

    private void Awake()
    {
        Invoke("ActivateThunder", activationTime);
    }

    IEnumerator ThunderRoutine()
    {
        //Time currently passed.
        var timer = 0f;
        //Maximum light intensity.
        var max = thunderObject.intensity;

        thunderObject.gameObject.SetActive(true);

        while (timer < duration)
        {
            timer += Time.deltaTime;

            //Adjusts light intensity depending on animation curve and time passed.
            thunderObject.intensity = lightningCurve.Evaluate(timer / duration) * max;

            yield return null;
        }

        //Resets light intensity back to start value.
        thunderObject.intensity = max;

        thunderObject.gameObject.SetActive(false);

        lightningRoutine = null;
    }

    private void ActivateThunder()
    {
        if (lightningRoutine == null)
        {
            lightningRoutine = StartCoroutine(ThunderRoutine());
        }
    }
}
