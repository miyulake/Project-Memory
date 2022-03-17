using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependentAnimator : AnimatorBase
{
    [Header("Box Pattern Gizmo:")]
    [SerializeField] private bool drawBoxPattern = false;
    [SerializeField] private Vector3 boxOffset = Vector3.up;
    [SerializeField] private float boxSize = 1;

    private void Start()
    {
        SetDefaults();
    }

    public void Animate(float speed, float amplitude)
    {
        animationTimer += Time.deltaTime * speed;

        SetFrame(animationTimer, amplitude);

        if (animationTimer > 1)
        {
            animationTimer = 0;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (drawBoxPattern && !IsPlaying)
        {
            float patternSize;

            patternSize = (boxSize / positionPath.BiggestAmplitude) / 2;

            DrawPath(boxOffset, patternSize, false);

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position + boxOffset, Vector3.one * boxSize);
        }
    }
}
