using UnityEngine;


public class ScrollTexture : MonoBehaviour
{
    [Header("Scrolling Direction")]
    [SerializeField] private bool scrollX;
    [SerializeField] private bool scrollY;

    [Header("Scrolling Settings")]
    [Tooltip("Mirror's the scrolling direction.")]
    [SerializeField] private bool mirror;

    [Tooltip("Controls the scrolling speed.")]
    [SerializeField] private float scrollSpeed = 0.5f;

    private void Update()
    {
        float offsetX = Time.time * scrollSpeed;
        float offsetY = Time.time * scrollSpeed;

        if (mirror)
        {
            offsetX = -offsetX;
            offsetY = -offsetY;
        }

        if(scrollX && !scrollY)
        {
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, 0);
        }
        else if (scrollY && !scrollX)
        {
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offsetY);
        }
        else if (scrollY && scrollX)
        {
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
}
