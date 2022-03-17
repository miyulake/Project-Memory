using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public Transform Head { get => headPivot; }

    [Header("Player Specific:")]
    [SerializeField] private Transform headPivot;

    private Vector3 rotation;
    private float pitch;

    public void RotateView(Vector2 input)
    {
        Vector3 targetRotation;

        //  Calculating Input.
        {
            targetRotation = input * rotationSpeed;
        }

        //  Applying To Variables.
        {
            if (rotationSmoothening > 0)
            {
                rotation = Vector3.Lerp(rotation, targetRotation, Time.deltaTime / rotationSmoothening);
            }
            else
            {
                rotation = targetRotation;
            }

            //  While the horizontal mouse input will be added to the player's Y rotation, the yaw rotation is a float value, and is applied to the head pivot.
            pitch += rotation.y;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
        }

        //  Applying Movement.
        {
            transform.Rotate(Vector3.up * rotation.x);
            headPivot.transform.localRotation = Quaternion.Euler(pitch, transform.localRotation.y, transform.localRotation.z);
        }
    }
}

