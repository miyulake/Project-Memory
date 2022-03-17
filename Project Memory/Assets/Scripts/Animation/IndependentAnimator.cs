using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentAnimator : AnimatorBase
{
    [Header("Independent Animation Settings:")]
    [SerializeField] private bool debugPlay = false;
    [Space]
    [Min(0)] [SerializeField] private float animationSpeed = 1;
    [Min(0)] [SerializeField] private float animationAmplitude = 1;

    private Coroutine animationRoutine;

    private void Update()
    {
        if (debugPlay && !IsPlaying)
        {
            PlayAnimation(true);
        }
        else if (!debugPlay && IsPlaying)
        {
            StopAnimationIfPlaying();
        }
    }

    #region Play & Stop
    /// <summary>
    /// Plays the animation set in the inspector.
    /// </summary>
    /// <param name="loop">Whether to loop the animation.</param>
    public void PlayAnimation(bool loop)
    {
        StopAnimationIfPlaying();

        SetDefaults();
        animationRoutine = StartCoroutine(AnimationRoutine(loop));

        IsPlaying = true;
    }

    /// <summary>
    /// Stops the animation if it is current playing.
    /// </summary>
    public void StopAnimationIfPlaying()
    {
        if (IsPlaying)
        {
            StopCoroutine(animationRoutine);
            ReturnToDefaults();

            animationTimer = 0;
            IsPlaying = false;
        }
    }
    #endregion

    private IEnumerator AnimationRoutine(bool loop)
    {
        //  Coroutine only executes while loop if the play independent bool is set to true.
        while (debugPlay)
        {
            animationTimer += Time.deltaTime * animationSpeed;

            SetFrame(animationTimer, animationAmplitude);

            //  If the end of the animation is reached..
            if (animationTimer > 1)
            {
                //  Execute the while loop again the next frame if 'loop' is set to true.
                if (loop)
                {
                    animationTimer = 0;
                }

                //  Stop animation if set to false.
                else
                {
                    StopAnimationIfPlaying();
                }
            }

            yield return null;
        }
    }

    protected override void OnDrawGizmos()
    {
        DrawPath(Vector3.zero, animationAmplitude);
    }
}
