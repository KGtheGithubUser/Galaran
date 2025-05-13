using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeController : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeInDuration = 1.5f;
    [SerializeField] private float fadeOutDuration = 1.0f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private void Start()
    {
        if (fadePanel != null)
        {
            // Start with the screen fully black
            Color startColor = fadePanel.color;
            startColor.a = 1f;
            fadePanel.color = startColor;
            
            // Fade in at the start
            StartCoroutine(FadeIn());
        }
    }
    
    public void FadeOut()
    {
        if (fadePanel != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutCoroutine());
        }
    }
    
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        Color color = fadePanel.color;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeInDuration;
            float evaluatedValue = fadeCurve.Evaluate(normalizedTime);
            
            color.a = 1f - evaluatedValue;
            fadePanel.color = color;
            
            yield return null;
        }
        
        // Ensure we reach fully transparent
        color.a = 0f;
        fadePanel.color = color;
    }
    
    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0;
        Color color = fadePanel.color;
        
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeOutDuration;
            float evaluatedValue = fadeCurve.Evaluate(normalizedTime);
            
            color.a = evaluatedValue;
            fadePanel.color = color;
            
            yield return null;
        }
        
        // Ensure we reach fully opaque
        color.a = 1f;
        fadePanel.color = color;
    }
}
