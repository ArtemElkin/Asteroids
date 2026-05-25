# Asteroids

---
## Entities
- GameArea
- Spaceship
- Asteroid
- AsteroidFragment
- Plate
- Weapon
---
## Core Services
- Player
	- PlayerSave
		- score
	- PlayerModel
		- score
- Input system
	- MovementInputService
	- FireInputService
- Config system
	- IConfig
	- IConfigProvider
	- ResourcesConfigProvider
	- AddresablesConfigProvider
- Ads System
	- IAdsService
	- YandexAdsService
- Analytics System
	- IAnalyticsService
	- FirebaseAnalyticsService
- Tools
	- CustomPool< T >
	- FactoryWithPool< T >
	- ScreenBoundsCalculator

---
### Common
- BounceOnCollisionHandler
- RandomPositionGenerator
- RandomDirectionGenerator

### Spaceship
Subsystems:
- HealthController
- InvulnerabilityHandler
	- InvulnerabilityTimer
- SpaceshipMovementController
	- SpaceshipInertiaHandler
	- SpaceshipAccelerationHandler
- SpaceshipCollisionHandler
	- BounceOnCollisionHandler
- AttackHandler
	- WeaponSwitcher

Auxiliary:
- SpaceShipSpawner
- SpaceshipConfig
	- maxHealth
	- maxSpeed
- SpaceshipMovementConfig
	- inertiaMultiplier
	- accelerationMultiplier

### Asteroid
Subsystems:
- AsteroidMovementController
	- RandomPositionGenerator
	- RandomDirectionGenerator
- AsteroidCollisionHandler
	- BounceOnCollisionHandler
- AsteroidDestructor

Auxiliary:
- AsteroidSpawner
- AsteroidDespawner
- AsteroidConfig
	- fragmentsCount
	- speed

### AsteroidFragment
Subsystems:
- AsteroidMovementController
	- RandomPositionGenerator
	- RandomDirectionGenerator
- AsteroidFragmentCollisionHandler
	- BounceOnCollisionHandler

Auxiliary:
- AsteroidFragmentSpawner
- AsteroidFragmentDespawner
- AsteroidFragmentConfig
	- speed

### Plate
Subsystems:
- PlateMovementController
	- TargetFollower
- PlateCollisionHandler

Auxiliary:
- PlateSpawner
- PlateDespawner
- PlateConfig
	- speed

### GameArea
- BoundsWarper
- GameAreaConfig
	- size

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





