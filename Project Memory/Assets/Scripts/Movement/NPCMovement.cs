using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
    private Vector3 targetPos;

    //  Rotation Components.
    private float yawShift;

    public virtual void RotateCharacter(int input)
    {
        float targetRotation;

        //  Calculating Desired Movement.
        {
            input = Mathf.Clamp(input, -1, 1);
            targetRotation = input * (rotationSpeed * 360);
        }

        //  Applying To Variable.
        {
            if (movementSmoothening > 0)
            {
                yawShift = Mathf.Lerp(yawShift, targetRotation, Time.deltaTime / rotationSmoothening);
            }
            else
            {
                yawShift = targetRotation;
            }
        }

        //  Applying Movement.
        {
            transform.Rotate(Vector3.up * (yawShift * Time.deltaTime));
        }
    }
}
