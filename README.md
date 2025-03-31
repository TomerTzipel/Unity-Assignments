Submitted by Tomer Tzipel, Ivgeny Fleishman & Itay Cohen,

Final Projcet:




















HW3
Notice that all scripts are in the HW3 namespace and the proper scene is under the HW3 folder

Same controls as HW2 but now there is a model and animation!

1. The Alien dude
2. We have 8 animations: Idle,Walk,Run,Heal,Invincible,SlowTime,Hurt and Death.
3+4. The Hurt animation and the movement animations (the blend tree) have their animations speed multiplied by the player's health precentage.
        The paramter is updated in the PlayerAnimationHandler script.
5+6. In the Upper body layer we have a sub state machine that checks the type of power up activated through the enum (passed as int) and then transisions into the proper animation.
        *We need to use buffer animations in the layer and sub state machine because you can't condions triggers going out of the Enter state.
7+8. The blend tree handles the movement animantions: Idle, walking and running depending on the player's movespeed from the navMeshAgent.
9+10. There is a upper body layer where the hurt and power pick up animations happen so the player can keep moving with the legs when doing his reaction to the event.
          The weight of the layer changes depending on the amount of damage taken divide by a set threshold for the hurt animation. The power ups animation set it back to 1 for them.
11. All the parameters are handled by the PlayerAnimationHandler script.
12. The death animation has a StateMachineBehaviour that enables a blood particle effect on the players and ends it when the state finishes.
13. I think we did, hopefully :D

No bonuses this time :(

*Some code changed in HW2 scripts to accomodate the PlayerAnimationHandler script, should only be new events for the animations.

HW2
Notice that all scripts are in the HW2 namespace and the proper scene is under the HW2 folder

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
