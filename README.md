# Audio Virtualization

Audio Virtualization is a real-time 3D audio visualization demo built in Unity. The application extracts frequency spectrum data from an audio source using Fast Fourier Transform (FFT) analysis, partitions the spectrum into eight frequency bands with smoothing buffers, and maps band intensities to 3D visual parameters including cube transforms, material emission brightness, dynamic point light intensity, and procedural cube spawning on peak amplitude. This project is a personal prototype/demo project, not a maintained product, with the last substantial work completed in 2022.

## Features

- **Spectrum Analysis & Frequency Banding**: Samples 512 frequency data points via `AudioSource.GetSpectrumData` using a Blackman window, groups samples across 8 frequency bands, and computes smoothed buffer values to prevent abrupt visual drops (`Assets/Scripts/AudioPeer.cs`).
- **Peak Threshold Cube Spawning**: Monitors highest frequency band amplitudes and dynamically spawns physics-enabled cube primitives with `Rigidbody` components at a designated spawn point when normalized amplitude exceeds 0.75 (`Assets/Scripts/AudioPeer.cs`).
- **Parametric Cube Transform & Emission**: Modulates the Y-scale and material emission color of individual 3D cubes based on normalized frequency band levels or buffered values (`Assets/Scripts/ParamCube.cs`).
- **512-Cube Radial Array Visualization**: Spawns 512 cube instances arranged in a circular formation (spaced at -0.703125 degree increments) and scales their heights according to spectrum data buffers (`Assets/Scripts/Instatiate512Cubes.cs`).
- **Audio-Reactive Point Lighting**: Dynamically interpolates the intensity of a Unity `Light` component between configurable minimum and maximum limits driven by the target frequency band buffer (`Assets/Scripts/LightOnAudio.cs`).
- **Frame Rate Regulation**: Configures `Application.targetFrameRate` to 60 FPS upon initialization (`Assets/Scripts/Options.cs`).

## Tech Stack

- **Engine**: Unity 2020.3.20f1 (declared in `ProjectSettings/ProjectVersion.txt`)
- **Language & Runtime**: C# / Unity Mono Scripting Backend
- **Core Packages & Modules** (declared in `Packages/manifest.json`):
  - `com.unity.modules.audio` (1.0.0): Audio playback and FFT spectrum analysis
  - `com.unity.modules.physics` (1.0.0): 3D rigidbody physics simulation
  - `com.unity.textmeshpro` (3.0.6): TextMesh Pro UI text rendering
  - `com.unity.timeline` (1.4.8): Timeline sequencing
  - `com.unity.ugui` (1.0.0): Unity UI system
  - `com.unity.test-framework` (1.1.29): Unity Test Framework package

## Project Structure

| Path | Contents | Type |
| --- | --- | --- |
| `Assets/Scripts/` | 5 C# scripts implementing audio spectrum analysis, frequency band buffering, cube instantiation, lighting reaction, and frame rate settings | Own code |
| `Assets/Scenes/` | Single Unity scene `SampleScene.unity` containing the visualizer setup, camera, lights, AudioPeer, and cubes | Own code |
| `Assets/Prefabs/` | `audio cube.prefab` used as the template object for radial visualizer instances | Own code |
| `Assets/Materials/` | `AudioCube.mat` (standard material with emission) and `Black.mat` | Own code |
| `Assets/Models/` | `cube.fbx` 3D model asset | Own code / Asset |
| `Assets/Music Tracks/` | 9 audio files in `.wav` format (`Chunky_Monkey.wav`, `InfiniteDoors.wav`, `JumpyGame.wav`, `Potato.wav`, `StreetLove.wav`, `Stupid_Dancer.wav`, `SunnyDay.wav`, `Tiny_Blocks.wav`, `Zephyr.wav`) | Data (Audio tracks) |
| `Packages/` | `manifest.json` and `packages-lock.json` defining Unity Package Manager dependencies | Third-party / Package config |
| `ProjectSettings/` | Unity project configurations including `ProjectVersion.txt`, `AudioManager.asset`, and `EditorBuildSettings.asset` | Engine configuration |

## Running / Building

### Prerequisites
- Unity Editor version `2020.3.20f1` installed via [Unity Hub](https://unity.com/download).

### Opening and Running the Project
1. Open **Unity Hub**.
2. Click **Open** > **Add project from disk** and select the repository root directory (`audio-virtualization`).
3. Launch the project using Unity Editor version `2020.3.20f1`.
4. Locate and open the primary scene at `Assets/Scenes/SampleScene.unity` (note: `ProjectSettings/EditorBuildSettings.asset` does not register any scenes in its build list; `SampleScene.unity` is the sole scene file in the repository).
5. In the Unity Editor hierarchy, verify that the `AudioPeer` GameObject has an `AudioSource` component assigned with a valid audio clip (by default configured with `Assets/Music Tracks/JumpyGame.wav`).
6. Press the **Play** button in the Unity Editor toolbar to run the visualization.

*Verification note*: The instructions above are derived directly from inspecting project assets and configuration files. As an automated terminal agent in an environment without a Unity runtime or display server, the project was not built or executed during this assessment.

## Repository Notes

- **Tests**: No automated test suites or test files exist in the repository (no tests under `Assets/Editor` or `Assets/Tests`, though `com.unity.test-framework` is listed in `Packages/manifest.json`).
- **CI/CD**: No continuous integration workflows or build pipelines are configured (no `.github/workflows` present).
- **Commit History**: Minimal history consisting of a single initial commit (`74cf742`, "1") committed on 2022-09-22.
- **Repository State**: This repository serves as a personal learning prototype exploring Unity audio spectrum analysis and reactive visual transforms. It is not an active or maintained production library.

## Third-Party Assets

| Path | What it provides | Licence |
| --- | --- | --- |
| `Assets/Music Tracks/` | 9 `.wav` background audio tracks used as input audio for the spectrum analyzer | No licence or attribution file found in repository |
