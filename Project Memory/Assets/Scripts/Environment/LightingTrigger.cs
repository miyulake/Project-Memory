using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct EnvironmentColor
    {
        public Color AmbientColor { get => ambientColor; }
        public Color FogColor { get => fogColor; }
        public static EnvironmentColor Empty { get => new EnvironmentColor(Color.black, Color.black); }

        [SerializeField] private Color ambientColor;
        [SerializeField] private Color fogColor;

        public EnvironmentColor(Color ambient, Color fog)
        {
            ambientColor = ambient;
            fogColor = fog;
        }

        /// <returns>The interpolated value between two EnvironmentColor structs.</returns>
        public static EnvironmentColor Lerp(EnvironmentColor a, EnvironmentColor b, float t)
        {
            var ambient = Color.Lerp(a.ambientColor, b.ambientColor, t);
            var fog = Color.Lerp(a.fogColor, b.fogColor, t);

            return new EnvironmentColor(ambient, fog);
        }
    }

    [Header("Lighting Settings")]
    [SerializeField] private EnvironmentColor targetEnvironment = new EnvironmentColor(Color.gray, Color.gray);
    [SerializeField] private AnimationCurve changePattern = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float targetDensity = 0.15f;
    [Min(0)] [SerializeField] private float changeTime = 1;

    private EnvironmentColor startEnv;

    [SerializeField] Camera cam;

    private float changeTimer = 0;
    private bool shouldChange = false;

    private void Update()
    {
        if (shouldChange)
        {
            Transition();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            OnPlayerEnter();   
        }
    }

    private void OnPlayerEnter()
    {
        shouldChange = true;
        startEnv = new EnvironmentColor(RenderSettings.ambientLight, RenderSettings.fogColor);
    }

    private void Transition()
    {
        if (changeTimer < changeTime)
        {
            float timePerc = changeTimer / changeTime;
            float progress = changePattern.Evaluate(timePerc);
            float currentDensity = RenderSettings.fogDensity;

            changeTimer += Time.deltaTime;

            EnvironmentColor env = EnvironmentColor.Lerp(startEnv, targetEnvironment, progress);
            RenderSettings.fogDensity = Mathf.Lerp(currentDensity, targetDensity, progress);

            RenderSettings.ambientLight = env.AmbientColor;
            RenderSettings.fogColor = env.FogColor;

            cam.GetComponent<Camera>();
            cam.backgroundColor = env.AmbientColor;


            return;
        }

        StopTransition();
    }

    private void StopTransition()
    {
        RenderSettings.ambientLight = targetEnvironment.AmbientColor;
        RenderSettings.fogColor = targetEnvironment.FogColor;

        startEnv = EnvironmentColor.Empty;
        changeTimer = 0;
        shouldChange = false;
    }
}
