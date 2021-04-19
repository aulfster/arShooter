using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextFading : MonoBehaviour
{
    [SerializeField]
    private float fadeInTime = 1.0f;
    [SerializeField]
    private float fadeOutTime = 1.0f;
    [SerializeField]
    private float textVisibleTime = 1.0f;
    [SerializeField]
    private float textInvisibleTime = 0.25f;

    private Text textComponent;
    private float timer = 0.0f;
    private string[] narrativeText = {
        "In 1991, a city-sized spacecraft crashes in Vancouver island on Earth. Over the course of 30 years the military organization "
            + "Banana Space Agency reverse-engineers its technology and rebuilds the spacecraft, naming it 'You Only Live Once'.",
        "The top pilot in the Banana Space Agency finally restored the spacecraft.\nMeanwhile, the alien mother battleship detects the "
            + "spacecraft and begins to rescue the spacecraft and the hibernated alien inside..."
    };
    private int currentTextIndex = 0;

    private enum FadeState
    {
        FadingIn,
        FadingOut,
        Visible,
        Invisible
    }
    private FadeState fadeState = FadeState.Invisible;

    private void Start()
    {
        textComponent = GetComponent<Text>();

        if (!textComponent)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // can ignore the update, it's just to make the coroutines get called for example
    void Update()
    {
        if (CheckShouldChangeScene())
        {
            return;
        }

        if (fadeState == FadeState.Invisible)
        {
            timer += Time.deltaTime / textInvisibleTime;

            if (timer >= 1.0f)
            {
                StartFadeIn();
            }
        }
        else if (fadeState == FadeState.Visible)
        {
            timer += Time.deltaTime / textVisibleTime;

            if (timer >= 1.0f)
            {
                StartFadeOut();
            }
        } 

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            if (fadeState == FadeState.Visible || fadeState == FadeState.FadingOut)
            {
                ++currentTextIndex;

                Debug.Log(currentTextIndex);

                if (CheckShouldChangeScene())
                {
                    return;
                }
            }

            if (fadeState == FadeState.FadingIn || fadeState == FadeState.FadingOut)
            {
                StopAllCoroutines();
            }

            Debug.Log(currentTextIndex);
            Debug.Log(narrativeText.Length);
            // Fast-forward to visible.
            textComponent.text = narrativeText[currentTextIndex];
            fadeState = FadeState.Visible;
            timer = 0.0f;
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 1.0f);
        }
    }

    private bool CheckShouldChangeScene()
    {
        if (currentTextIndex >= narrativeText.Length)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return true;
        }

        return false;
    }

    private void StartFadeIn()
    {
        textComponent.text = narrativeText[currentTextIndex];
        fadeState = FadeState.FadingIn;
        timer = 0.0f;
        StartCoroutine(FadeTextToFullAlpha(fadeInTime, textComponent));
    }

    private void StartFadeOut()
    {
        fadeState = FadeState.FadingOut;
        timer = 0.0f;
        StartCoroutine(FadeTextToZeroAlpha(fadeOutTime, textComponent));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        fadeState = FadeState.Visible;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        fadeState = FadeState.Invisible;
        ++currentTextIndex;
    }
}
