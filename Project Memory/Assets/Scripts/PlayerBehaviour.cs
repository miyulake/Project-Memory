using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerMovement Movement { get; private set; }

    private DependentAnimator headAnimator;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();

        headAnimator = Movement.Head.GetComponent<DependentAnimator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //  TO DO: If bobhead if executed after rotate player, the rotation of the mouse will be overwritten by the constantly running animation.
        BobHead();

        ManageMovement();
    }

    private void ManageMovement()
    {
        //  Movement.
        {
            Vector2 input;

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input = input.normalized;

            Movement.MoveCharacter(input);
        }

        //  Rotation.
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

            Movement.RotateView(input);
        }

        //  Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Movement.Jump();
        }
    }

    private void BobHead()
    {
        if (headAnimator != null && Movement.OnGround)
        {
            float animationStrength = Movement.HorizontalVelocity.magnitude / Movement.MovementSpeed;

            //  TO DO: Animate function runs every frame, even while the player is standing still.
            headAnimator.Animate(animationStrength, animationStrength);
        }
    }
}
