# Nightmaze

A first-person mini escape-horror game built with **Unity 6** and deployed as a playable **WebGL** build.

**Play live:** https://masterchiefproject.github.io/Nightmaze/

Nightmaze focuses on short-form exploration and escape gameplay: navigate a hostile environment, survive hazards, use doors and portals, cross moving platforms, react to horror audio cues, and reach the exit before a death trigger sends you back to the retry flow.

## Highlights

- Unity 6 project using the Universal Render Pipeline
- First-person movement, mouse look, and jumping
- Door interactions driven by trigger volumes and animation parameters
- Portal-based teleportation with CharacterController and Rigidbody support
- Horizontal and vertical moving-platform hazards
- Death triggers with randomized audio and retry scene flow
- Win trigger and dedicated victory scene
- Main menu, quit confirmation, retry, and win-menu scenes
- Trigger-based environmental horror audio
- Playable WebGL build published through GitHub Pages
- Custom WebGL shell with dark mode by default
- Persistent light/dark theme preference for the browser shell
- Responsive browser presentation and fullscreen control
- Static CI checks for WebGL packaging and deployment files

## Unity version

The project is built with:

```text
Unity 6000.0.47f1
```

Use this editor version when opening the project to avoid unnecessary asset and scene serialization changes.

## Controls

| Action | Control |
| --- | --- |
| Move | WASD / Arrow Keys |
| Look | Mouse |
| Jump | Space |
| Fullscreen | Browser shell Fullscreen button |

A desktop browser with keyboard and mouse is recommended for the WebGL build.

## Gameplay flow

Nightmaze uses four production scenes:

| Scene | Purpose |
| --- | --- |
| `MainMenuScene` | Entry point and play/exit controls |
| `GameScene` | Main first-person escape level |
| `TryAgainScene` | Failure/retry flow |
| `WinScene` | Victory flow |

The player progresses through the main environment while interacting with trigger-driven doors, teleport portals, moving platforms, environmental audio, death volumes, and the final win trigger.

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
│   ├── WebGLTemplates/
│   │   └── Nightmaze/
│   │       ├── index.html
│   │       └── TemplateData/
│   │           ├── shell.js
│   │           └── style.css
│   └── ... third-party art/audio packages
├── Packages/
├── ProjectSettings/
├── docs/                         # Deployable GitHub Pages WebGL build
│   ├── Build/
│   ├── TemplateData/
│   ├── .nojekyll
│   └── index.html
├── tests/
│   └── repository.test.mjs
├── ASSET-NOTICE.md
└── README.md
```

## Runtime architecture

The gameplay scripts are intentionally small and component-oriented.

- `Movement` provides CharacterController-based first-person movement, mouse look, gravity, and jumping.
- `FirstPlayerController` provides an alternative Rigidbody-based first-person controller used by compatible scene setups.
- `Door` toggles an Animator parameter when the player enters or leaves its trigger.
- `Portal` teleports the player while safely handling CharacterController, Rigidbody, and transform-only objects.
- `PlatformLeftRight` and `PlatformUpDown` provide deterministic moving-platform motion.
- `Death` handles death triggers, optional randomized death audio, cursor release, and transition to `TryAgainScene`.
- `Win` releases the cursor and transitions to `WinScene`.
- `MainMenu`, `QuitMenu`, `TryAgainMenu`, and `WinMenu` coordinate scene-level UI flow.
- `BabyScript` provides trigger-driven horror audio feedback.
- `Globals` centralizes scene names, tags, and animator parameter constants.

## WebGL frontend

The deployable build lives in `docs/` and is served by GitHub Pages.

The browser shell is separate from the Unity game UI. It adds:

- dark mode as the default
- a persistent Light mode / Dark mode switch
- a responsive game frame
- accessible loading and error states
- a source-code link
- fullscreen support
- a CSS-based loading indicator that does not depend on generated Unity template artwork

The selected shell theme is stored locally under:

```text
nightmaze-web-theme-v1
```

Changing the browser-shell theme does not modify the lighting or visuals inside the Unity game itself.

## Custom Unity WebGL template

The production browser shell is also stored as a Unity custom WebGL template under:

```text
Assets/WebGLTemplates/Nightmaze/
```

This prevents a future WebGL rebuild from reverting the site to Unity's default generated page.

Unity supports project WebGL templates from `Assets/WebGLTemplates/<TemplateName>`. The Nightmaze template uses Unity's build-time filename macros for the loader, data, framework, and WebAssembly files.

## Building the GitHub Pages version

A project build command is included in:

```text
Assets/Editor/NightmazeWebGLBuild.cs
```

In Unity, use:

```text
Nightmaze > Build WebGL for GitHub Pages
```

The command:

1. uses the `Nightmaze` custom WebGL template
2. builds all enabled production scenes
3. cleans and rebuilds the `docs/` output directory
4. writes `docs/.nojekyll`
5. fails explicitly if the Unity build reports an error

After a successful build:

```bash
git add -A
git commit -m "Update Nightmaze WebGL build"
git push
```

GitHub Pages should publish from:

```text
Branch: main
Folder: /docs
```

## Local WebGL testing

Do not open `docs/index.html` directly with `file://`. WebAssembly and compressed Unity build files must be served over HTTP.

From the repository root:

```bash
python -m http.server 8000 --directory docs
```

Then open:

```text
http://localhost:8000/
```

## Validation and CI

The repository includes lightweight CI for the committed WebGL deployment without requiring a Unity activation/license on GitHub-hosted runners.

The workflow validates:

- the deployed WebGL loader/data/framework/WASM files are present
- the current deployment uses the Nightmaze browser shell
- dark mode is the default
- the persistent theme switch is wired correctly
- the custom Unity WebGL template contains the required Unity build macros
- the expected Unity editor version is documented in the project
- the production scene list contains only the four active Nightmaze scenes
- the death-audio path is not disabled by unreachable code

Run the repository checks locally with Node.js 20 or newer:

```bash
node --check docs/TemplateData/shell.js
node --test tests/repository.test.mjs
```

These checks validate repository/deployment packaging. Full scene, physics, rendering, audio, and gameplay validation still requires the Unity Editor or a built player.

## Production cleanup

The production setup removes the stale URP template `Readme.asset` and removes the missing/disabled `SampleScene` entry from Unity's build settings. Unity-generated directories such as `Library`, `Temp`, `Obj`, `Logs`, `Build`, and `UserSettings` remain excluded by `.gitignore`.

## WebGL notes

- The committed WebGL build uses compressed `.unityweb` assets.
- The existing WebGL build profile has decompression fallback enabled, which is appropriate for static hosting such as GitHub Pages.
- Browser `Application.Quit()` cannot close a user's tab. The WebGL quit confirmation therefore returns control to the menu instead of presenting a dead quit action.
- The game is primarily designed for desktop keyboard/mouse input.

## Third-party assets

This project contains third-party Unity assets, art, audio, fonts, shaders, and editor tooling in addition to the original Nightmaze scripts and scenes.

The repository does not claim ownership of third-party content and does not relicense those assets. See [`ASSET-NOTICE.md`](ASSET-NOTICE.md) before redistributing or commercially using repository assets.

## Project status

Nightmaze is maintained as a compact portfolio game and WebGL demonstration. The production setup prioritizes reproducible Unity builds, a clean GitHub Pages presentation, explicit scene flow, safe browser behavior, and preservation of the original horror-game experience.
