using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFlicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer highlight;
    [SerializeField] private float lowOpacity = 0.5f;
    [SerializeField] private float highOpacity = 1;

    private void Update()
    {
        highlight.color = new Color(1, 1, 1, Random.Range(lowOpacity, highOpacity));
    }
}
