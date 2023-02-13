using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop } //외부에서 불러올때 사용하기 편하게 하기 = 열거형 사용

public class FadeInOut : MonoBehaviour
{

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private Image image;
    private FadeState fadestate;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        //StartCoroutine(Fade(1, 0)); //FadeIn
            
        OnFade(FadeState.FadeOut);

    }
    public void OnFade(FadeState state)
    {
        fadestate = state;

        switch (fadestate)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:

            case FadeState.FadeLoop:
                StartCoroutine(FadeControl());
                break;
        }
    }

    private IEnumerator FadeControl()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));

            if (fadestate == FadeState.FadeInOut)
            {
                break;
            }

        }
    }

    /* public void FadeIn()
     {
         Color color = image.color;

         if (color.a > 0)
         {
             color.a -= Time.deltaTime;
         }
         image.color = color;
     }

     /*public void FadeOut()
     {
         Color color = image.color;
         color.a = 0;

         if(color.a < 1)
         {
             color.a += Time.deltaTime;
         }
         image.color = color;
     }*/

    private IEnumerator Fade(float start, float end)
    {
        float currnetTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currnetTime += Time.deltaTime;
            percent = currnetTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
      
    }

}
