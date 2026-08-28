# Nightmaze

[![Unity](https://img.shields.io/badge/Unity-6000.0.47f1-black?logo=unity)](https://unity.com/)
[![WebGL](https://img.shields.io/badge/WebGL-browser%20build-5b7fff)](https://masterchiefproject.github.io/Nightmaze/)

**Nightmaze** is a compact first-person escape-horror game built with Unity 6 and the Universal Render Pipeline.

**Playable WebGL build:** https://masterchiefproject.github.io/Nightmaze/

The game focuses on short-form exploration and escape gameplay. The player moves through a hostile environment with trigger-driven doors, teleport portals, moving platforms, environmental audio, death hazards, and a dedicated retry/victory flow.

## Highlights

- Unity 6 and Universal Render Pipeline
- First-person movement, mouse look, and jumping
- Trigger-driven animated doors
- Portal-based teleportation
- CharacterController and Rigidbody-compatible teleport handling
- Horizontal and vertical moving platforms
- Death triggers with randomized audio
- Environmental horror audio cues
- Dedicated main menu, retry, and victory scenes
- Custom WebGL frontend
- Persistent dark/light browser theme
- Fullscreen support
- GitHub Pages deployment
- Automated repository and WebGL packaging checks

## Controls

| Action | Control |
| --- | --- |
| Move | `WASD` / Arrow Keys |
| Look | Mouse |
| Jump | `Space` |
| Fullscreen | Browser shell Fullscreen control |

The browser build is designed primarily for desktop keyboard and mouse input.

## Game flow

Nightmaze uses four production scenes:

| Scene | Role |
| --- | --- |
| `MainMenuScene` | Entry point and menu flow |
| `GameScene` | Main escape level |
| `TryAgainScene` | Failure and retry flow |
| `WinScene` | Completion flow |

The main level combines movement challenges, portals, moving platforms, trigger volumes, horror audio, death zones, and the final escape trigger.

## Runtime architecture

The gameplay layer is component-oriented:

- `Movement` implements CharacterController-based first-person movement, gravity, jumping, and mouse look.
- `FirstPlayerController` provides an alternative Rigidbody-based controller for compatible scene setups.
- `Door` controls Animator state from player trigger entry and exit.
- `Portal` handles teleportation for CharacterController, Rigidbody, and transform-only objects.
- `PlatformLeftRight` and `PlatformUpDown` implement deterministic moving-platform motion.
- `Death` handles failure triggers, optional randomized death audio, cursor release, and scene transition.
- `Win` handles completion and transition to the victory scene.
- `MainMenu`, `QuitMenu`, `TryAgainMenu`, and `WinMenu` coordinate scene-level UI.
- `BabyScript` provides trigger-based environmental audio.
- `Globals` centralizes shared scene names, tags, and Animator parameter constants.

## Project structure

```text
Nightmaze/
├── .github/
│   └── workflows/
│       └── webgl-shell.yml
├── Assets/
│   ├── Editor/
│   │   └── NightmazeWebGLBuild.cs
│   ├── Scenes/
│   ├── Scripts/
│   ├── Settings/
│   └── WebGLTemplates/
│       └── Nightmaze/
├── Packages/
├── ProjectSettings/
├── docs/                         # Deployable GitHub Pages build
├── tests/
│   └── repository.test.mjs
├── ASSET-NOTICE.md
└── README.md
```

## WebGL frontend

The deployed browser shell is stored both in the committed `docs/` build and as a Unity custom template under:

```text
Assets/WebGLTemplates/Nightmaze/
```

The shell provides:

- dark mode by default
- persistent light/dark preference
- responsive game framing
- accessible loading and error states
- source-code link
- fullscreen support
- CSS-based loading presentation

The browser theme is independent of Unity scene lighting and is stored locally under:

```text
nightmaze-web-theme-v1
```

## Unity and build workflow

The project targets:

```text
Unity 6000.0.47f1
```

The production WebGL build is generated through:

```text
Nightmaze > Build WebGL for GitHub Pages
```

`Assets/Editor/NightmazeWebGLBuild.cs` packages the enabled production scenes with the custom browser template, recreates the `docs/` output, writes `.nojekyll`, and reports Unity build failures explicitly.

The generated browser build is served over HTTP for local testing:

```bash
python -m http.server 8000 --directory docs
```

Local URL:

```text
http://localhost:8000/
```

## Validation

The repository includes lightweight automated checks that do not require Unity activation on GitHub-hosted runners.

The checks cover:

- deployed WebGL loader/data/framework/WASM files
- custom browser shell integration
- dark-mode default and persisted theme state
- Unity WebGL template macros
- expected Unity editor version
- production scene list
- critical gameplay-source invariants

Local repository verification:

```bash
node --check docs/TemplateData/shell.js
node --test tests/repository.test.mjs
```

Scene behavior, physics, rendering, and audio are additionally validated through the Unity Editor or a built player.

## Deployment

GitHub Pages serves the committed `docs/` build from the `main` branch.

**Live build:** https://masterchiefproject.github.io/Nightmaze/

## WebGL behavior

The committed WebGL build uses compressed Unity assets with decompression fallback for static hosting. Browser `Application.Quit()` cannot close the host tab, so the WebGL menu flow returns control to the game UI rather than relying on a desktop quit operation.

## Third-party assets

Nightmaze contains third-party Unity art, audio, fonts, shaders, and editor tooling in addition to the project-specific scripts and scenes.

The repository does not claim ownership of third-party content and does not relicense those assets. See [`ASSET-NOTICE.md`](ASSET-NOTICE.md) for redistribution information.

## Project status

Nightmaze is maintained as a compact Unity portfolio project demonstrating first-person controller integration, trigger-driven level mechanics, scene-flow design, WebGL packaging, browser presentation, and automated deployment validation.
