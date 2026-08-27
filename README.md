# Hack and Slash RPG

High-quality Unity foundation for an online-capable sci-fi hack-and-slash RPG set in Neo-Tokyo.

## Direction

Third-person real-time combat in a dense neon city. The player uses responsive movement, chained abilities, stagger windows, finishers and readable enemy telegraphs. The first release target is a polished solo vertical slice; online co-op arrives after the combat foundation is proven.

## Open in Unity

1. Install Unity `6000.5.9f1` with Android and Windows build modules.
2. Open this folder as a Unity project.
3. Allow Package Manager to resolve the pinned packages. The project targets URP `17.5.0`.
4. Put licensed production assets in `Assets/Art/Incoming` and document them in `Docs/Art/AssetRegister.md`.
5. Create scenes under `Assets/Scenes`: `Boot`, `MainMenu`, `NeonStreet_VerticalSlice`.

When first opening the migrated project, let Unity finish the package and asset import before saving. Commit any Unity-generated lockfile changes separately after confirming the Console is clear.

## Code foundation

- `Assets/Scripts/Combat/AbilityDefinition.cs` — authored abilities with animation/VFX references.
- `Assets/Scripts/Combat/CombatActor.cs` — health/damage boundary.
- `Assets/Scripts/Combat/ThirdPersonCombatController.cs` — movement and ability input.
- `Assets/Scripts/AI/EnemyBrain.cs` — chase/attack presentation boundary.
- `Assets/Editor/AssetReadinessWindow.cs` — production asset intake helper.

## Repository policy

Do not commit `Library/`, `Temp/`, `Logs/`, `Obj/`, `Build/` or unlicensed asset packages. See `Docs/AI/UnityProjectContext.md` for the current architecture and limitations.
