Submitted by Tomer Tzipel & Itay Cohen,

HW2
Notice that all cripts are inthe HW2 namespace and the proper scene is under the HW2 folder

You play by moving with left click, destroying bullets adds score, hp will be lost when the player is green.
You can flash with F towards the mouse position

Purple Cube = Invincibility
Green Cube  = Heal
Teal Cube  = Time Slow

Part 1:
1. This mechanic is implemented both in bullet to player damage and in PowerUp cube to player for activation. (Power Up is relevent for part 2)


Bullet hits player --(Trigger)--> Bullet --(OnBulletHit Action)--> PlayerController--(OnPlayerHit Event)--> PlayerHealthHandler 
(BulletManager subscribes PlayerController to the bullet's action when a bullet is spawned)

Player hits PowerUp --(Trigger)--> PowerUp --(Action)--> PlayerController--(Proper Action from EffectActions)--> To the methods who subscribed to the proper action (GameManager subscribes PlayerController to the powerup's action when a powerup is spawned)

2. In the prefabs folder you can find prefabs for both the player and the powerups.


3+4+B1. UnityActions examples (there are more than 3):
		1. PlayerController.OnPlayerHitAction passes an int
		2. PlayerHealthHandler.OnPlayerDeath passes nothing
		3 + B1. BulletHandler.OnBulletHit passes a BulletCollisionArgs, which is a struct we made (can be found in BulletHandler).

5+B2. UnityEvents examples (this time there are only 2):
		1. PlayerFlashHandler.OnPlayerFlash passes nothing, makes the audioSource of the flash SFX
		2. PlayerController.OnPlayerHit passes an int which is the damage taken from the bullet for the score to be added and plays the proper SFX.
		B2. PlayerController.OnPlayerHitAction passes all his listeners to PlayerController.OnPlayerHit in start so only the event is invoked

Part 2:
1. PowerUpGenerator is a ScriptableObject with a list with all 3 powerups.
2. GameManager holds the prefab from 1. (A better name would be PowerUpManager but you asked to call it GameManager)
3+4+B3. PowerUpGenerator has a method that return a powerup by enum and also a random powerup that is calculated by weights you can give the ScriptableObject, you just nned to make sure it adds up to 100.
5. All powerup prefabs get their value from the ScriptableObject PowerUpSettings. (so we did it for all 3 using the same scriptable)




HW1
Move player with mouse right click,
Reach the yellow target before the red AI,
Brown slows you, purple speeds you.
