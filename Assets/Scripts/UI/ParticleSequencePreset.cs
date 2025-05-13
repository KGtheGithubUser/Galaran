using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides preset particle configurations for the intro cutscene
/// </summary>
[CreateAssetMenu(fileName = "ParticleSequencePreset", menuName = "DungeonDuel/Particle Sequence Preset")]
public class ParticleSequencePreset : ScriptableObject
{
    [SerializeField] private List<ParticleSequenceItem> particleSequence = new List<ParticleSequenceItem>();
    
    public List<ParticleSequenceItem> ParticleSequence => particleSequence;
    
    /// <summary>
    /// Creates a dramatic intro sequence with the game's existing prefabs
    /// </summary>
    public void CreateDramaticIntroPreset(ParticleSystem vaseBreakPrefab)
    {
        particleSequence.Clear();
        
        // Initial dust particles from the floor
        var item1 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 0.5f,
            spawnPosition = new Vector3(0, 0.1f, 0),
            useRandomPosition = false,
            duration = 3f,
            intensityMultiplier = 0.6f,
            sizeMultiplier = 0.8f
        };
        particleSequence.Add(item1);
        
        // Magical particles from pillars
        var item2 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 1.2f,
            spawnPosition = new Vector3(-3, 2, 0),
            useRandomPosition = true,
            randomPositionRange = new Vector3(1f, 0.5f, 1f),
            duration = 5f,
            intensityMultiplier = 1.2f,
            sizeMultiplier = 1.5f
        };
        particleSequence.Add(item2);
        
        // Mirror effect on the opposite side
        var item3 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 1.5f,
            spawnPosition = new Vector3(3, 2, 0),
            useRandomPosition = true,
            randomPositionRange = new Vector3(1f, 0.5f, 1f),
            duration = 4.5f,
            intensityMultiplier = 1.2f,
            sizeMultiplier = 1.5f
        };
        particleSequence.Add(item3);
        
        // Ceiling dust/debris
        var item4 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 3.0f,
            spawnPosition = new Vector3(0, 5, 0),
            useRandomPosition = true,
            randomPositionRange = new Vector3(4f, 0.5f, 4f),
            duration = 3f,
            intensityMultiplier = 0.7f,
            sizeMultiplier = 1.0f
        };
        particleSequence.Add(item4);
        
        // Door effect (approaching entrance)
        var item5 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 4.5f,
            spawnPosition = new Vector3(0, 2, 5),
            useRandomPosition = false,
            duration = 3f,
            intensityMultiplier = 1.5f,
            sizeMultiplier = 1.2f
        };
        particleSequence.Add(item5);
        
        // Dramatic final burst
        var item6 = new ParticleSequenceItem
        {
            particleSystem = vaseBreakPrefab,
            startDelay = 7.0f,
            spawnPosition = new Vector3(0, 1, 0),
            useRandomPosition = false,
            duration = 4f,
            intensityMultiplier = 2.0f,
            sizeMultiplier = 1.8f
        };
        particleSequence.Add(item6);
        
        // Small ambient particles throughout the scene
        for (int i = 0; i < 5; i++)
        {
            var ambientItem = new ParticleSequenceItem
            {
                particleSystem = vaseBreakPrefab,
                startDelay = Random.Range(0.5f, 5.0f),
                spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(0.5f, 4f), Random.Range(-5f, 5f)),
                useRandomPosition = false,
                duration = Random.Range(2f, 5f),
                intensityMultiplier = Random.Range(0.3f, 0.8f),
                sizeMultiplier = Random.Range(0.4f, 0.9f)
            };
            particleSequence.Add(ambientItem);
        }
    }
    
    /// <summary>
    /// Applies color modifications to create a magical effect
    /// Use this in the Editor to customize particle colors
    /// </summary>
    public void ApplyMagicalColors(ParticleSystem baseParticle)
    {
        if (baseParticle == null)
            return;
            
        // This is just a template - you'll need to apply these in Unity
        // Color over lifetime to create magical effects
        
        // Main color tints
        Color[] magicalColors = new Color[]
        {
            new Color(0.8f, 0.2f, 0.8f), // Purple
            new Color(0.2f, 0.6f, 0.9f), // Blue
            new Color(0.9f, 0.4f, 0.1f), // Orange
            new Color(0.1f, 0.8f, 0.3f), // Green
            new Color(0.9f, 0.8f, 0.2f)  // Gold
        };
        
        // You'll apply these colors to your particle system instances
        Debug.Log("Apply these colors in the Unity Editor to your particle systems:");
        foreach (Color color in magicalColors)
        {
            Debug.Log($"R: {color.r}, G: {color.g}, B: {color.b}");
        }
    }
}
