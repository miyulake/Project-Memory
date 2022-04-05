using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private CharacterMovement moveScript;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [SerializeField] private KeyCode dashInput;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashInput))
        {
            StartCoroutine(DashAction());
        }
    }

    IEnumerator DashAction()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.characterController.Move(moveScript.HorizontalVelocity * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
