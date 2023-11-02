using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float fadeTime = 1.0f;

    public CanvasGroup mainMenu;
    public CanvasGroup settingsGroup;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn(mainMenu));
    }

    public void Play()
    {
        SceneManager.LoadScene("World1 - 1");
    }

    IEnumerator FadeOutThenIn(CanvasGroup outGroup, CanvasGroup inGroup)
    {
        yield return FadeOut(outGroup);
        yield return FadeIn(inGroup);
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        group.alpha = 1f;
        Tween tween = group.DOFade(0, fadeTime).SetEase(Ease.OutCubic);
        yield return tween.WaitForCompletion();
        group.gameObject.SetActive(false);
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        group.alpha = 0f;
        group.gameObject.SetActive(true);
        Tween tween = group.DOFade(1, fadeTime).SetEase(Ease.InCubic);
        yield return tween.WaitForCompletion();
    }

    public void Settings()
    {
        StartCoroutine(FadeOutThenIn(mainMenu, settingsGroup));
    }

    public void BackToMainMenu()
    {
        StartCoroutine(FadeOutThenIn(settingsGroup, mainMenu));
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
}
