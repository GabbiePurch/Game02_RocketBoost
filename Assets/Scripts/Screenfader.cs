using UnityEngine;
using UnityEngine.UI;

public class Screenfader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;

    private bool isFadingIn = false;
    private bool isFadingOut = false;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1);
        FadeOut();
    }
    void Update()
    {
        if (isFadingIn)
        {
            Color color = fadeImage.color;
            color.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = color;

            if (color.a >= 1f)
            {
                color.a = 1f;
                fadeImage.color = color;
                isFadingIn = false;
            }
        }
        else if (isFadingOut)
        {
            Color color = fadeImage.color;
            color.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = color;

            if (color.a <= 0f)
            {
                color.a = 0f;
                fadeImage.color = color;
                isFadingOut = false;
            }
        }
    }

    public void FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
    }

    public void FadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
    }
}