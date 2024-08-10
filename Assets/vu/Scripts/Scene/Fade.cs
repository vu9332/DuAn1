using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = Faderoutine(1);
        StartCoroutine(fadeRoutine);
    }
    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = Faderoutine(0);
        StartCoroutine(fadeRoutine);
    }
    private IEnumerator Faderoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }

    }
}
