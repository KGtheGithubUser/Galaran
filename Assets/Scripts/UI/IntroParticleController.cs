using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleSequenceItem
{
    public ParticleSystem particleSystem;
    public float startDelay;
    public Vector3 spawnPosition;
    public bool useRandomPosition;
    public Vector3 randomPositionRange = new Vector3(5f, 3f, 5f);
    public float duration = 3f;
    public float intensityMultiplier = 1f;
    [Range(0.1f, 2f)]
    public float sizeMultiplier = 1f;
}

public class IntroParticleController : MonoBehaviour
{
    [Header("Particle Prefabs")]
    [SerializeField] private ParticleSystem vaseBreakParticles; // Reference to DungeonVaseBreak1 prefab
    
    [Header("Particle Settings")]
    [SerializeField] private List<ParticleSequenceItem> particleSequence = new List<ParticleSequenceItem>();
    [SerializeField] private int maxSimultaneousParticles = 5;
    [SerializeField] private float masterIntensity = 1.5f;
    [SerializeField] private bool useRenderTexture = true;
    
    [Header("Camera Effects")]
    [SerializeField] private Camera renderCamera;
    [SerializeField] private RenderTexture renderTexture;
    
    private List<ParticleSystem> activeParticleSystems = new List<ParticleSystem>();
    private Coroutine sequenceCoroutine;
    
    private void Start()
    {
        if (useRenderTexture && renderCamera != null && renderTexture != null)
        {
            renderCamera.targetTexture = renderTexture;
        }
    }
    
    public void PlayIntroSequence()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
        }
        
        sequenceCoroutine = StartCoroutine(PlayParticleSequence());
    }
    
    public void StopIntroSequence()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
        }
        
        StopAllParticles();
    }
    
    private IEnumerator PlayParticleSequence()
    {
        foreach (ParticleSequenceItem item in particleSequence)
        {
            if (item.particleSystem != null)
            {
                // Wait for the start delay
                yield return new WaitForSeconds(item.startDelay);
                
                // Spawn the particle system
                Vector3 spawnPos = item.spawnPosition;
                if (item.useRandomPosition)
                {
                    spawnPos += new Vector3(
                        Random.Range(-item.randomPositionRange.x, item.randomPositionRange.x),
                        Random.Range(-item.randomPositionRange.y, item.randomPositionRange.y),
                        Random.Range(-item.randomPositionRange.z, item.randomPositionRange.z)
                    );
                }
                
                // Create the particle system
                ParticleSystem newSystem = Instantiate(item.particleSystem, spawnPos, Quaternion.identity);
                
                // Apply custom settings
                var main = newSystem.main;
                main.startSizeMultiplier *= item.sizeMultiplier;
                
                var emission = newSystem.emission;
                emission.rateOverTimeMultiplier *= item.intensityMultiplier * masterIntensity;
                
                // Manage active systems
                activeParticleSystems.Add(newSystem);
                if (activeParticleSystems.Count > maxSimultaneousParticles)
                {
                    if (activeParticleSystems[0] != null)
                    {
                        Destroy(activeParticleSystems[0].gameObject);
                    }
                    activeParticleSystems.RemoveAt(0);
                }
                
                // Let it play for its duration
                StartCoroutine(DestroyAfterTime(newSystem, item.duration));
            }
        }
    }
    
    private IEnumerator DestroyAfterTime(ParticleSystem ps, float time)
    {
        yield return new WaitForSeconds(time);
        
        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            
            // Wait for particles to finish
            yield return new WaitForSeconds(ps.main.duration + ps.main.startLifetime.constantMax);
            
            if (ps != null)
            {
                activeParticleSystems.Remove(ps);
                Destroy(ps.gameObject);
            }
        }
    }
    
    private void StopAllParticles()
    {
        foreach (ParticleSystem ps in activeParticleSystems)
        {
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }
        
        activeParticleSystems.Clear();
    }
    
    // Special effect for vase break during cutscene
    public void TriggerVaseBreakEffect(Vector3 position)
    {
        if (vaseBreakParticles != null)
        {
            ParticleSystem vaseEffect = Instantiate(vaseBreakParticles, position, Quaternion.identity);
            
            // Apply enhanced settings for cutscene
            var main = vaseEffect.main;
            main.startSizeMultiplier *= 1.5f;
            
            var emission = vaseEffect.emission;
            emission.rateOverTimeMultiplier *= 2f * masterIntensity;
            
            // Auto destroy
            Destroy(vaseEffect.gameObject, vaseEffect.main.duration + vaseEffect.main.startLifetime.constantMax);
        }
    }
}
