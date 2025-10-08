using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Screenfader : MonoBehaviour
{
    private Image sceneFadeImage;

    void Awake()
    {
        sceneFadeImage = GetComponent<Image>();

    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);

        gameObject.SetActive(true);

        yield return FadeCoroutine(startColor, targetColor, duration);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        Color targetColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);

        gameObject.SetActive(true);

        yield return FadeCoroutine(startColor, targetColor, duration);
    }


    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elpasedPrecentage = 0;


        while (elpasedPrecentage < 1)
        {
            elpasedPrecentage = elapsedTime / duration;
            sceneFadeImage.color = Color.Lerp(startColor, targetColor, elpasedPrecentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        sceneFadeImage.color = targetColor;
    }


}