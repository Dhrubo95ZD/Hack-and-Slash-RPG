# Hack and Slash RPG — Unity Project Context

## Project summary

Production-oriented Unity foundation for a third-person sci-fi hack-and-slash RPG. The first milestone is a polished solo vertical slice; multiplayer is isolated behind Netcode-ready boundaries and is not required for the first playable.

## Confirmed foundation

- Unity 6.0.43f1
- Universal Render Pipeline 17.0.3
- Input System 1.11.2
- Cinemachine 3.1.0
- Addressables 1.21.21
- Netcode for GameObjects 2.2.0 (integration boundary only)
- Unity Test Framework 1.5.1

## Architecture

Feature folders under `Assets/Scripts` own focused runtime systems. Combat data is authored as ScriptableObjects (`AbilityDefinition`); runtime actors expose small event-based health boundaries. The player controller owns input, movement and ability dispatch. Enemy AI owns chase presentation and delegates damage to `CombatActor`.

## Asset and scene policy

Real licensed assets are imported through `Assets/Art/Incoming`, registered in `Docs/Art/AssetRegister.md`, then moved into Addressables groups. No placeholder block art is intended for release scenes. Scenes and prefabs will be added with Unity Editor once the licensed character/environment package is selected.

## Validation

Unity Editor compilation and Play Mode validation are pending because no Unity Editor is attached to this workspace. The C# files are intentionally dependency-light and are ready for an Editor import pass.

## Next vertical-slice milestones

1. Import the Android protagonist and locomotion/attack animation set.
2. Build a cinematic third-person camera rig and one authored street arena.
3. Add hit-stop, telegraphs, stagger, finisher and boss phase state machines.
4. Add account/session services behind interfaces before online co-op.
