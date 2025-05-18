using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroCutsceneManager : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private PlayableDirector timelineDirector;
    
    [Header("Particle Systems")]
    [SerializeField] private List<ParticleSystem> introParticles = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem vaseBreakParticles;
    [SerializeField] private float particleDelay = 0.5f;
    
    [Header("Scene Transition")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float introDuration = 10f;
    [SerializeField] public Animator fadeAnimator;
    
    [Header("Sound")]
    [SerializeField] private AudioSource introMusic;
    
    private bool canSkip = false;
    
    private void Start()
    {
        // Initialize the cutscene
        StartCoroutine(PlayIntroSequence());
        StartCoroutine(EnableSkipping());
    }
    
    private void Update()
    {
        // Allow skipping with any key press
        if (canSkip && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            SkipIntro();
        }
    }
    
    private IEnumerator EnableSkipping()
    {
        // Wait a bit before allowing skipping
        yield return new WaitForSeconds(1.5f);
        canSkip = true;
    }
    
    private IEnumerator PlayIntroSequence()
    {
        // Start the timeline
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
        
        // Play intro music
        if (introMusic != null)
        {
            introMusic.Play();
        }
        
        // Trigger particle systems with delay between each
        foreach (ParticleSystem ps in introParticles)
        {
            ps.Play();
            yield return new WaitForSeconds(particleDelay);
        }
        
        // Special vase break effect in the middle of the sequence
        yield return new WaitForSeconds(introDuration / 2);
        if (vaseBreakParticles != null)
        {
            vaseBreakParticles.Play();
        }
        
        // Wait for the full intro duration
        yield return new WaitForSeconds(introDuration / 2);
        
        // Transition to main menu
        TransitionToMainMenu();
    }
    
    public void SkipIntro()
    {
        // Stop all coroutines
        StopAllCoroutines();
        
        // Stop particles
        foreach (ParticleSystem ps in introParticles)
        {
            ps.Stop();
        }
        
        if (vaseBreakParticles != null)
        {
            vaseBreakParticles.Stop();
        }
        
        // Transition to main menu
        TransitionToMainMenu();
    }
    
    private void TransitionToMainMenu()
    {
        // Play fade out animation if available
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
            StartCoroutine(LoadAfterFade());
        }
        else
        {
            // Load main menu directly
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
    
    private IEnumerator LoadAfterFade()
    {
        // Wait for fade animation
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
