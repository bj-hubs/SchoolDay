using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fade;

    public void FadeIn()
    {
        fade.CrossFadeAlpha(0, 0.5f, false);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0.5f);
        fade.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        fade.gameObject.SetActive(true);
        fade.CrossFadeAlpha(0, 0, false);
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        fade.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}
