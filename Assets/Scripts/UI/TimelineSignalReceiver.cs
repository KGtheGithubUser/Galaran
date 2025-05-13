using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineSignalReceiver : MonoBehaviour
{
    [SerializeField] private IntroParticleController particleController;
    [SerializeField] private GameObject[] gameObjectsToActivate;
    [SerializeField] private Light[] dynamicLights;
    
    // Called via Timeline Signal
    public void TriggerParticleSequence()
    {
        if (particleController != null)
        {
            particleController.PlayIntroSequence();
        }
    }
    
    // Called via Timeline Signal
    public void TriggerVaseBreak(Vector3 position)
    {
        if (particleController != null)
        {
            particleController.TriggerVaseBreakEffect(position);
        }
    }
    
    // Called via Timeline Signal
    public void ActivateGameObject(int index)
    {
        if (gameObjectsToActivate != null && index >= 0 && index < gameObjectsToActivate.Length)
        {
            gameObjectsToActivate[index].SetActive(true);
        }
    }
    
    // Called via Timeline Signal
    public void DeactivateGameObject(int index)
    {
        if (gameObjectsToActivate != null && index >= 0 && index < gameObjectsToActivate.Length)
        {
            gameObjectsToActivate[index].SetActive(false);
        }
    }
    
    // Called via Timeline Signal to change lighting during cutscene
    public void SetLightIntensity(int lightIndex, float intensity)
    {
        if (dynamicLights != null && lightIndex >= 0 && lightIndex < dynamicLights.Length)
        {
            dynamicLights[lightIndex].intensity = intensity;
        }
    }
    
    // Called via Timeline Signal to change lighting color during cutscene
    public void SetLightColor(int lightIndex, Color color)
    {
        if (dynamicLights != null && lightIndex >= 0 && lightIndex < dynamicLights.Length)
        {
            dynamicLights[lightIndex].color = color;
        }
    }
}
