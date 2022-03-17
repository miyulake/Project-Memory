using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessTester : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float updateStep = 0.1f;
    [SerializeField] private int sampleDataLength = 1024;

    [SerializeField] private float currentUpdateTime = 0f;

    [SerializeField] private float clipLoudness;
    [SerializeField] private float[] clipSampleData;

    [SerializeField] private GameObject musicObject;
    [SerializeField] private float sizeFactor = 1;

    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    [SerializeField] private float smoothness;

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }

    private void Update()
    {
        currentUpdateTime += Time.deltaTime;
        
        if(currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);

            clipLoudness = Sum(clipSampleData);
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);

            musicObject.transform.localScale = Vector3.Lerp(musicObject.transform.localScale, Vector3.one * clipLoudness * sizeFactor, Time.deltaTime / smoothness);
        }

        Invoke("DestroyMusicObject", 1f);
    }

    private float Sum(float[] collection)
    {
        float total = 0;
        
        foreach (var sample in collection)
        {
            total += Mathf.Abs(sample);
        }
        return total / collection.Length;
    }

    private void DestroyMusicObject()
    {
        if (clipLoudness <= 0)
        {
            Destroy(musicObject);
        }
    }

}
