using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Anchor Point")]
    [SerializeField] private GameObject anchor;

    [Header("Target Rotation")]
    [SerializeField] private Vector3 rotationOffset;

    [Header("Open Time")]
    [SerializeField] private float duration;

    [Space]
    [SerializeField] private bool open;

    [Space]
    [SerializeField] private float radius;

    private Coroutine lerpCoroutine;

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        bool keyPressed = Input.GetKeyDown(KeyCode.E);
        bool inRange = playerDistance <= radius;

        if (keyPressed && inRange && open == true)
        {
            Toggle(rotationOffset, duration, anchor);
        }
    }
    private void Toggle(Vector3 eulerOffset, float duration, GameObject itemToLerp)
    {
        if (lerpCoroutine != null) return;

        lerpCoroutine = StartCoroutine(LerpPosition(eulerOffset, duration, itemToLerp));
    }

    IEnumerator LerpPosition(Vector3 eulerOffset, float duration, GameObject itemToLerp)
    {
        float time = 0;

        Quaternion startRotation = itemToLerp.transform.localRotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(eulerOffset);

        bool isOpen = open;

        if (!isOpen)
        {
            open = true;
        }

        while (time < duration)
        {
            itemToLerp.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        if (isOpen)
        {
            open = false;
        }

        lerpCoroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
