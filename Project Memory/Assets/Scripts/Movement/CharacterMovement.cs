using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public bool OnGround
    {
        get
        {
            Ray groundRay;
            float castDistance;

            groundRay = new Ray(transform.position + (Vector3.up * characterController.radius), Vector3.down);
            castDistance = characterController.skinWidth * 1.01f;

            if (Physics.SphereCast(groundRay, characterController.radius, castDistance))
            {
                return true;
            }

            return false;
        }
    }

    public Vector3 Velocity { get => velocity; }
    public Vector3 HorizontalVelocity { get => new Vector3(velocity.x, 0, velocity.z); }
    public float MovementSpeed { get => movementSpeed; }

    [Header("Basic Movement:")]
    [Range(0, 40)] [SerializeField] protected float movementSpeed = 3;
    [Range(0, 3)] [SerializeField] protected float rotationSpeed = 3;
    [Space]
    [Min(0)] [SerializeField] private float jumpStrength;

    [Header("Advanced Attributes:")]
    [Range(0, 3)] [SerializeField] protected float movementSmoothening = 0.1f;
    [Range(0, 3)] [SerializeField] protected float rotationSmoothening = 0.1f;
    [Space]
    [SerializeField] protected float gravityScale = 1;

    //  Movement Components.
    private Vector3 velocity;
    private Vector2 movement;
    private float fall;

    //  References.
    private CharacterController characterController;

    private void Awake()
    {
        if (TryGetComponent(out CharacterController controller))
        {
            characterController = controller;
        }
        else
        {
            Debug.LogError($"{gameObject.name} needs an active 'CharacterController' component in order for it to move.");
        }
    }

    public void MoveCharacter(Vector2 input)
    {
        //  Horizontal Movement.
        {
            Vector2 targetMovement;

            //  Calculating Desired Movement.
            {
                Vector2 forwardDir;
                Vector2 rightDir;

                Vector2 zMovement;
                Vector2 xMovement;

                if (input != Vector2.zero)
                {
                    input = input.normalized;
                }

                forwardDir = new Vector2(transform.forward.x, transform.forward.z);
                rightDir = new Vector2(transform.right.x, transform.right.z);

                zMovement = forwardDir * input.y;
                xMovement = rightDir * input.x;

                targetMovement = (xMovement + zMovement) * movementSpeed;
            }

            //  Applying To Variable.
            {
                if (movementSmoothening > 0)
                {
                    movement = Vector2.Lerp(movement, targetMovement, Time.deltaTime / movementSmoothening);
                }
                else
                {
                    movement = targetMovement;
                }
            }
        }

        //  Vertical Movement.
        {
            float gravity = 9.8f * gravityScale;

            if (!OnGround)
            {
                fall -= (gravity * Time.deltaTime);
            }
            else if (fall < 0)
            {
                fall = 0;
            }
        }

        //  Applying Movement.
        {
            velocity = new Vector3(movement.x, fall, movement.y);

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (OnGround)
        {
            fall += jumpStrength;
        }
    }
}
