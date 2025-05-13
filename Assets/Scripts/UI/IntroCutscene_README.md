# DungeonDuel Intro Cutscene Implementation Guide

This document explains how to set up the intro cutscene for DungeonDuel using the provided scripts and your existing prefabs.

## Overview

The intro cutscene is a particle-heavy sequence that serves as the game's initial presentation. It utilizes:
- Timeline for sequencing
- Multiple particle systems for visual effects
- Existing prefabs for game assets
- Camera effects and render textures for enhanced visuals

## Setup Instructions

### 1. Create the Intro Scene

1. In Unity, create a new scene named `IntroScene`
2. Add it to your build settings (File > Build Settings) and position it before your main menu scene

### 2. Set Up the Scene Hierarchy

Create the following hierarchy:
```
- IntroCutscene (empty GameObject)
  - CutsceneManager
  - ParticleController
  - Timeline
  - UI
    - Canvas
      - FadePanel (Image)
  - Cameras
    - MainCamera
    - ParticleRenderCamera
  - Lights
    - DirectionalLight
    - SpotLight1
    - SpotLight2
  - Environment
    - FloorPlane
    - Props
      - VaseObject (using DungeonVase1.prefab)
      - Walls (using DungeonWall1.prefab)
      - Pillars (using DungeonPillar1.prefab)
```

### 3. Configure Components

#### CutsceneManager GameObject:
1. Add the `IntroCutsceneManager.cs` script
2. Set the `Main Menu Scene Name` to your main menu scene
3. Reference the Timeline Director component
4. Set intro duration to 10-15 seconds
5. Reference the Fade Animator

#### ParticleController GameObject:
1. Add the `IntroParticleController.cs` script
2. Assign the `DungeonVaseBreak1.prefab` to the Vase Break Particles field
3. Configure Particle Sequence Items:
   - Create 5-8 sequence items with varying start delays and positions
   - Configure size and intensity multipliers for dynamic effects
4. Set Master Intensity to 1.5 for dramatic effect
5. Enable Use Render Texture and assign the dedicated render texture

#### Timeline GameObject:
1. Add a Playable Director component
2. Create a new Timeline asset and assign it to the director
3. Add Signal Receiver component and assign the `TimelineSignalReceiver.cs` script
4. Reference the ParticleController
5. Add object references to gameObjectsToActivate array
6. Assign lights to the dynamicLights array

#### UI Canvas:
1. Set Render Mode to Screen Space - Overlay
2. Add an Image child named FadePanel (black, full screen)
3. Add the `ScreenFadeController.cs` script to the Canvas
4. Reference the FadePanel image

#### Cameras:
1. Configure MainCamera to frame the scene
2. Add a second camera (ParticleRenderCamera) dedicated to rendering particles
3. Create a RenderTexture asset (1920x1080) and assign to ParticleRenderCamera

### 4. Timeline Setup

Create a timeline with the following tracks:
1. **Animation Track**: For camera movements and object animations
2. **Activation Track**: To activate/deactivate objects at specific times
3. **Signal Track**: With signals at key moments:
   - 0:00 - Start particle sequence
   - 3:00 - Trigger vase break effect
   - 8:00 - Final particle burst

### 5. Particle Effects Setup

Use the existing `DungeonVaseBreak1.prefab` as your base particle system, and create variations:
1. Duplicate and modify the prefab for different effects
2. Adjust colors, sizes, emission rates, and textures
3. Place them strategically in the scene
4. Configure the IntroParticleController's sequence to trigger them in order

### 6. Lighting

1. Set up dynamic lighting with color changes during the sequence
2. Configure the directional light for overall illumination
3. Use spot lights to highlight specific elements during the cutscene
4. Add light intensity/color changes in the timeline

## Final Checklist

- Ensure all scripts are properly referenced
- Check that the Timeline plays correctly
- Verify all particles are visible and render properly
- Confirm scene transitions work (fade in/out)
- Test the skip functionality

## Technical Notes

- The intro uses a separate render texture for particles to achieve better visual quality
- For best performance, limit particle count to what your target platform can handle
- All effects use the existing prefabs, just configured for more dramatic presentation
- The system is designed to smoothly transition to your main menu
