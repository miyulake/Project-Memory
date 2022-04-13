using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCycle : MonoBehaviour
{
    [SerializeField] private RawImage image;

    [SerializeField] private Texture[] textureList;

    [SerializeField] private float time;

    private float timer;
    private int currentIndex;

    private void Update()
    {
        Iterate(Time.deltaTime);
    }

    public void Cycle()
    {
        currentIndex++;

        if (currentIndex >= textureList.Length)
        {
            currentIndex = 0;
        }

        image.texture = textureList[currentIndex];
    }

    public void Iterate(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= time)
        {
            Cycle();
            timer = 0;
        }
    }
}