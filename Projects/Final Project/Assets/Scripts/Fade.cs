using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration;
    public bool fadeComplete = false;

    public void FadeToBlack() => StartCoroutine(Fade(.0f, 1f));
    public void FadeFromBlack() => StartCoroutine(Fade(1f, .0f));

    private IEnumerator Fade(float start, float end)
    {
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
        canvasGroup.alpha = end;
        fadeComplete = true;
    }
}