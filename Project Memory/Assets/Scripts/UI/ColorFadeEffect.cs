using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorFadeEffect : MonoBehaviour
{
    [Space]
    [SerializeField] private float SpeedMod;

    [Header("Image Values")]
    [SerializeField] private Image screenImage;
    [SerializeField] private Color targetImageColor;

    [Header("Text Values")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color fadeInColor = new Color(1, 1, 1, 1);
    [SerializeField] private Color fadeOutColor = new Color(1, 1, 1, 0);

    [Header("UI Type")]
    [SerializeField] private bool screenFade;
    [SerializeField] private bool textFadeIn;

    private void Update()
    {
        if (screenFade && !textFadeIn) screenImage.color = Color.Lerp(screenImage.color, targetImageColor, SpeedMod * Time.deltaTime);

        if (textFadeIn && !screenFade)
        {
            text.color = Color.Lerp(text.color, fadeInColor, SpeedMod * Time.deltaTime);
            if(text.color.a >= 0.9)
            {
                textFadeIn = false;
            }
        }

        if (!textFadeIn && !screenFade)
        {
            text.color = Color.Lerp(text.color, fadeOutColor, SpeedMod * Time.deltaTime);
        }

        if (screenFade && textFadeIn)
        {
            Debug.LogError("Two UI types are active!");
        }
    }
}
