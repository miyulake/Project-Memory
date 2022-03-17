using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEquipped : MonoBehaviour
{
    [Header("Currently Equipped")]
    [SerializeField] private GameObject currentHolding;

    [Header("Target Position")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    [Header("Unequip Time")]
    [SerializeField] private float duration;

    [Space]
    [SerializeField] private bool holding;
    private Coroutine lerpCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && holding == true)
        {
            Toggle(positionOffset, rotationOffset, duration, currentHolding);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && holding == false)
        {
            Toggle(-positionOffset, -rotationOffset, duration, currentHolding);
        }
    }

    private void Toggle(Vector3 positionOffset, Vector3 eulerOffset, float duration, GameObject itemToLerp)
    {
        if (lerpCoroutine != null) return;

        lerpCoroutine = StartCoroutine(LerpPosition(positionOffset, eulerOffset, duration, itemToLerp));
    }

    IEnumerator LerpPosition(Vector3 positionOffset, Vector3 eulerOffset, float duration, GameObject itemToLerp)
    {
        float time = 0;

        Vector3 startPosition = itemToLerp.transform.localPosition;
        Quaternion startRotation = itemToLerp.transform.localRotation;

        Vector3 targetPosition = startPosition + positionOffset;
        Quaternion targetRotation = startRotation * Quaternion.Euler(eulerOffset);

        bool isHolding = holding;

        if (!isHolding)
        {
            holding = true;
            itemToLerp.SetActive(true);
        }

        while (time < duration)
        {
            itemToLerp.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            itemToLerp.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        if (isHolding)
        {
            holding = false;
            itemToLerp.SetActive(false);
        }

        lerpCoroutine = null;
    }
}