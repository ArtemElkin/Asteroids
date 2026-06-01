# Asteroids

---
## Entities
- Bounds
- Spaceship
- SpaceshipClone
- Asteroid
- AsteroidFragment
- UFO
- Weapon
---
## Core
- Ads
  - AdUnitIdsConfig
  - IAdsService
  - MockAdsService
- Analytics
    - IAnalyticsService
- Config
  - IConfig
  - IConfigProvider
- Input
    - IMovementInputService
    - IFireInputService
- Math
  - Math
  - Vector2
  - Vector3
- Physics
  - IMovable
  - InitialMovementData
  - IPositionable
  - IReadOnlyPositionable
  - IReadOnlyRotatable
  - IRotatable
  - IWarpable
  - MovementModel
  - Physics
- Player
    - PlayerModel
	- PlayerSave
    - PlayerSaveController
- Save
  - ISave
  - ISaveService
- Services
  - IRandomService
  - IScreenService
  - ITimeService
  - RandomService
- Signals
  - GameRestartedSignal
  - InitializeGameSignal
  - ISignalBus
  - MenuClickedSignal
  - StartGameClickedSignal
  - StartGameSignal
  - StopGameSignal
- Tools
  - PositionGenerator
  - Storage
---
### Common
- BaseMovementController
- BaseRotationController
- BaseSpawner
- IDrawable
- MovableView
- SpawnTimer

### Spaceship
- SpaceshipFacade
- SpaceshipHealthController
- SpaceshipInvulnerabilityHandler
	- InvulnerabilityTimer
- SpaceshipMovementController
- SpaceshipRotationController
- SpaceshipCollisionHandler
- SpaceshipAttackHandler
	- WeaponSwitcher

Auxiliary:
- SpaceshipConfig
- SpaceshipFactory
- SpaceshipMovementConfig
- SpaceshipSpawnData
- SpaceshipSpawner

### SpaceshipClone
- SpaceshipCloneFacade
- SpaceshipCloneFactory
- SpaceshipCloneSpawnData

### Asteroid
- AsteroidFacade
- AsteroidMovementController
- AsteroidCollisionHandler
- AsteroidDestructor

Auxiliary:
- AsteroidConfig
- AsteroidDespawner
- AsteroidFactory
- AsteroidInstaller
- AsteroidSpawnData
- AsteroidSpawner

### AsteroidFragment
- AsteroidMovementController
- AsteroidFragmentCollisionHandler

Auxiliary:
- AsteroidFragmentConfig
- AsteroidFragmentDespawner
- AsteroidFragmentSpawner

### Bounds
- BoundsChecker
- BoundsInstaller
- BoundsService
- BoundsWarper
- BoundType

### UFO
- UFOFacade
- UFOMovementController
- UFORotationController
- UFOCollisionHandler
- UFOTargetFollower

Auxiliary:
- UFOConfig
- UFODespawner
- UFOFactory
- UFOInstaller
- UFOSpawnData
- UFOSpawner

### Weapon
- WeaponType (enum)
- IWeapon
- ProjectileWeapon
	- ProjectileSpawner
	- ProjectileDespawner
	- Projectile
		- ProjectileMovementController
		- ProjectileCollisionHandler
		- ProjectileConfig
			- speed
	- ProjectileWeaponConfig
		- maxProjectilesPerSecond
- LaserWeapon
	- LaserSpawner
	- LaserDespawner
	- Laser
		- LaserCollisionHandler
	- LaserReloadTimer
	- LaserWeaponConfig
		- maxCharges
		- maxChargesPerSeconds
		- rechargeTime

---
## INFRASTRUCTURE
- Ads
- Analytics 
  - FirebaseAnalyticsService
- Config
  - ResourcesConfigProvider 
  - AddresablesConfigProvider
- DI
  - BootstrapperInstaller
  - ProjectInstaller
- Factories
  - CustomPool
  - IFactory
- Input
    - StandaloneInputHandler
    - MobileInputHandler
- Lifecycle
  - Bootstrapper
  - GameplayStarter
- Save
  - PlayerPrefsSaveService
- Services
  - SceneLoadService
  - ScreenService
  - UnityTimeService
- Signals
  - ZenjectSignalBus
- Tools
  - VectorExtensions

---
## UI
- InfoPanel
	- SpaceshipPositionView
	- SpaceshipRotationView
	- SpaceshipSpeedView
	- LaserChargesView
	- LaserReloadTimeView
- MobileInput
	- JoystickView
	- AttackButton
	- SwitchWeaponButton