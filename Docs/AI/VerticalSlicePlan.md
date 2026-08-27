# First Playable Vertical Slice

The repository foundation is now ready for a focused solo combat slice targeting Windows and Android.

## Player experience

1. Spawn an authored android protagonist in a neon street arena.
2. Move with the Input System, a touch virtual stick, or keyboard during development.
3. Orbit a Cinemachine camera and use a large touch-safe lock-on action.
4. Use three authored abilities with animation and VFX references.
5. Read enemy telegraphs, dodge or reposition, and land hits with brief hit-stop feedback.
6. Defeat a small encounter and transition to a boss arena.

## Runtime boundaries

- CombatActor remains the health and damage boundary.
- CombatTargeting owns local target selection only.
- EnemyTelegraph owns warning presentation and emits timing events; it does not apply damage.
- CombatImpactFeedback is local feedback and must not become gameplay authority.
- MobileInputBridge is UI-facing input state and stays resolution-independent.
- Netcode integration should replicate validated combat results, not input-side damage.

## Android requirements

- Add Android and Windows modules to Unity 6000.0.43f1.
- Use portrait-safe margins only for menus; use landscape for combat.
- Keep primary touch actions inside thumb reach and at least 48 dp in hit area.
- Profile on a mid-range Android device before adding post-processing or dense VFX.
- Use Addressables for environment, actor, animation, VFX and audio bundles.
- Keep a device quality tier with reduced shadows and capped particle counts.

## Editor setup still required

- Import licensed or generated android, enemy, environment, animation, VFX and audio assets.
- Create Boot, MainMenu and NeonStreet_VerticalSlice scenes.
- Add a Cinemachine camera rig and wire the player camera reference.
- Create AbilityDefinition assets and assign animation/VFX references.
- Place target markers, enemy telegraph meshes and touch UI in prefabs.
- Validate in Unity Editor and Play Mode on Windows and Android.

## Art quality gate

No primitive-only release presentation. Every visible actor, arena, attack effect and UI surface must be backed by a licensed or generated asset with a recorded source and usage rights in Docs/Art/AssetRegister.md.
