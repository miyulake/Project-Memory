using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorBase : MonoBehaviour
{
    public bool IsPlaying { get; protected set; }

    [Header("Paths:")]
    [SerializeField] protected ThreeDimensionalPath positionPath;
    [SerializeField] protected ThreeDimensionalPath rotationPath;
    [SerializeField] protected ThreeDimensionalPath scalePath;

    [Header("Animation Status:")]
    [Range(0, 1)] [SerializeField] protected float animationTimer = 0;

    [Header("Debug:")]
    [SerializeField] private bool drawWorldPattern = true;
    [Range(0, 0.25f)] [SerializeField] private float linePointSize = 0.1f;
    [Min(0)] [SerializeField] private int lineResolution = 50;
    [Space]
    [SerializeField] private Color pathStartColor = Color.red;
    [SerializeField] private Color pathEndColor = new Color(0, 0.5f, 1);
    [Space]
    [SerializeField] private Vector3 estimatedMovement = Vector3.zero;

    private Vector3 standardPosition;
    private Vector3 standardEulerAngles;
    private Vector3 standardScale;

    private bool standardValuesSet;

    #region Default Transform Functions
    /// <summary>
    /// Sets the defualt transform variables to the current transform state.
    /// </summary>
    protected void SetDefaults()
    {
        standardPosition = transform.localPosition;
        standardEulerAngles = transform.localEulerAngles;
        standardScale = transform.localScale;

        standardValuesSet = true;
    }

    /// <summary>
    /// Returns the transform state to be set to earlier saved standard values.
    /// </summary>
    protected void ReturnToDefaults()
    {
        transform.localPosition = standardPosition;
        transform.localEulerAngles = standardEulerAngles;
        transform.localScale = standardScale;

        ResetDefaults();
    }

    /// <summary>
    /// Resets the standard values to a new Vector3.
    /// </summary>
    private void ResetDefaults()
    {
        standardPosition = new Vector3();
        standardEulerAngles = new Vector3();
        standardScale = new Vector3();

        standardValuesSet = false;
    }
    #endregion

    /// <summary>
    /// Sets the object's transform to the values dependent on the percentage of the timeline.
    /// </summary>
    /// <param name="percent">The timeline percent of the animation.</param>
    protected void SetFrame(float percent, float amplitude = 1)
    {
        transform.localPosition = standardPosition + positionPath.GetPathValue(percent, amplitude);
        transform.localEulerAngles = standardEulerAngles + rotationPath.GetPathValue(percent, amplitude);

        ///  The animating of the scale is different, because instead of adding position or rotation to the already set values,the scale animation values represents a set size.
        ///  For instance a value of 0 on the position path means that no value gets added to the position of the object on that axis, but a value of 0 on the scale path mean that the animator wants the size to 0 at that point in time.
        transform.localScale =  scalePath.GetPathValue(percent, amplitude, transform.localScale);
    }

    #region Debug
    protected void DrawPath(Vector3 pathOffset, float pathScale, bool drawMoveEstimation = true)
    {
        int lineRes = lineResolution;

        if (lineRes > 0)
        {
            //  The previous point position should be set to a value for it to be accepted into the code, even for the first iteration.
            Vector3 previousPointPos = Vector3.zero;

            void DrawPoint(float percent)
            {
                Color pointColor;
                Vector3 pointPos;
                Vector3 pointSize;
                bool firstPoint;
                bool lastPoint;

                //  Calculating Values.
                {
                    firstPoint = percent == 0;
                    lastPoint = percent == 1;

                    if (standardValuesSet)
                    {
                        pointPos = standardPosition;
                    }
                    else
                    {
                        pointPos = transform.localPosition;
                    }

                    if (transform.parent != null)
                    {
                        pointPos += transform.parent.position;
                    }

                    pointPos += positionPath.GetPathValue(percent, pathScale);
                    pointPos += pathOffset;

                    if (drawMoveEstimation)
                    {
                        pointPos += estimatedMovement * percent;
                    }

                    pointSize = scalePath.GetPathValue(percent, pathScale, 1);
                    pointSize *= linePointSize;

                    pointColor = Color.Lerp(pathStartColor, pathEndColor, percent);
                }

                //  Drawing Gizmos.
                {
                    if (!firstPoint)
                    {
                        Gizmos.color = pointColor;
                        Gizmos.DrawLine(previousPointPos, pointPos);
                    }

                    if (linePointSize > 0)
                    {
                        Gizmos.color = new Color(pointColor.r, pointColor.g, pointColor.b, pointColor.a * 0.1f);
                        Gizmos.DrawCube(pointPos, pointSize);

                        Gizmos.color = pointColor;
                        Gizmos.DrawWireCube(pointPos, pointSize);
                    }
                }

                //  Set the previous point position for the next iteration.
                previousPointPos = pointPos;
            }

            for (int i = 0; i < lineRes; i++)
            {
                float iterationPercent;

                //  Progress is a float from 0 to 1 determined by how far into the for loop this iteration takes place.
                iterationPercent = (float)i / (float)lineRes;

                DrawPoint(iterationPercent);
            }

            DrawPoint(1);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (drawWorldPattern)
        {
            DrawPath(Vector3.zero, 1);
        }
    }
    #endregion

    #region Datatypes
    [System.Serializable]
    public struct ThreeDimensionalPath
    {
        public float BiggestAmplitude
        {
            get
            {
                float biggestAmplitude = x.Amplitude;

                if (y.Amplitude > biggestAmplitude)
                    biggestAmplitude = y.Amplitude;

                if (z.Amplitude > biggestAmplitude)
                    biggestAmplitude = z.Amplitude;

                return biggestAmplitude;
            }
        }

        /// <returns>The evaluation of the path based on the 'percent' value, combined with other data.</returns>
        /// <param name="percentage">Where on the path timeline the value should be evaluated from.</param>
        /// <param name="amplitude">The multiplier for the returned value.</param>
        /// <param name="nullValue">The value to return in an axis of the Vector3 if the corresponding axis does not have animation data.</param>
        public Vector3 GetPathValue(float percentage, float amplitude, float nullValue = 0)
        {
            Vector3 nullVector = new Vector3(nullValue, nullValue, nullValue);

            return GetPathValue(percentage, amplitude, nullVector);
        }

        /// <returns>The evaluation of the path based on the 'percent' value, combined with other data.</returns>
        /// <param name="percentage">Where on the path timeline the value should be evaluated from.</param>
        /// <param name="amplitude">The multiplier for the returned value.</param>
        /// <param name="nullValue">The values to return in a Vector3 axis if the corresponding axis does not have animation data.</param>
        public Vector3 GetPathValue(float percentage, float amplitude, Vector3 nullValue)
        {
            float xValue = x.GetPathValue(percentage, amplitude, nullValue.x);
            float yValue = y.GetPathValue(percentage, amplitude, nullValue.y);
            float zValue = z.GetPathValue(percentage, amplitude, nullValue.z);

            return new Vector3(xValue, yValue, zValue);
        }

        public OneDimensionalPath x;
        public OneDimensionalPath y;
        public OneDimensionalPath z;
    }

    [System.Serializable]
    public struct OneDimensionalPath
    {
        public AnimationCurve Path { get => path; }
        public float Amplitude
        {
            get
            {
                if (path != null && path.length > 0)
                {
                    return pathAmplitude;
                }

                return 0;
            }
        }

        /// <returns>The evaluation of the path based on the 'percent' value, combined with other data.</returns>
        /// <param name="percentage">Where on the path timeline the value should be evaluated from.</param>
        /// <param name="amplitude">The multiplier for the returned value.</param>
        /// <param name="nullValue">The value to return if the axis does not have animation data.</param>
        public float GetPathValue(float percentage, float amplitude, float nullValue = 0)
        {
            //  The value return is the null value by default, unless a value is calculated
            float valueToReturn = nullValue;
            //  Only return a calculated value if the path has keyframes present, and the amplitude is higher than 0.
            bool calculatePath = pathAmplitude > 0 && path != null && path.length > 0;

            if (calculatePath)
            {
                valueToReturn = path.Evaluate(percentage) * pathAmplitude;

                //  Checking whether a multiplication calculation actually has to be done, might be more performant.
                if (amplitude != 1)
                {
                    valueToReturn *= amplitude;
                }
            }

            return valueToReturn;
        }

        [Tooltip("The one dimensional path of this axis. The line should preferably be kept between -1 and 1, as the amplitude of the path can be determined with the value below.")]
        [SerializeField] private AnimationCurve path;

        [Tooltip("The amplitude of the path of this axis. Leave on 0 to disable animation on this axis.")]
        [Min(0)] [SerializeField] private float pathAmplitude;
    }
    #endregion
}
