using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterBehaviour : MonoBehaviour
{
    protected CharacterMovement Movement { get; private set; }

    protected virtual void Awake()
    {
        if (TryGetComponent(out CharacterMovement movement))
        {
            Movement = movement;
        }
    }
}
