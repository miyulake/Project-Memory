using UnityEngine;
using UnityEngine.UI;


public class ScrollSprite : MonoBehaviour
{
    [Header("Scrolling Direction")]
    [SerializeField] private bool scrollX;
    [SerializeField] private bool scrollY;

    [Header("Scrolling Settings")]
    [Tooltip("Controls the scrolling speed.")]
    [SerializeField] private float scrollSpeed = 0.5f;

    private RawImage rawImage;
    
    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        Rect uvRect = rawImage.uvRect;

        if (scrollX && !scrollY)
        {
            uvRect.x += scrollSpeed * Time.deltaTime;
        }
        else if (scrollY && !scrollX)
        {
            uvRect.y += scrollSpeed * Time.deltaTime;
        }
        else if (scrollY && scrollX)
        {
            uvRect.y += scrollSpeed * Time.deltaTime;
            uvRect.x += scrollSpeed * Time.deltaTime;
        }

        rawImage.uvRect = uvRect;
    }
}