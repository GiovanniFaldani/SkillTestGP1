# SkillTestGP1
2D Platformer project for EH School

# Controls
A		: Move left
D		: Move right
Space	: Jump
F		: Attack/Grab power-up
P		: Pause

# Features:
The game features collectibles that act as powerups, allies and enemies:

## Collectibles:
Marked by colored circles, they are:
- Green	: heals 3 HP
- Yellow: invincibility for 5 seconds
- Blue	: 50% increased speed for 10 seconds

Collecting all the ones in the level will clear the game.

## Allies:
The frog ninja NPCs. They can't move but they will heal the player by 5 HP when close

## Enemies:
The mask wearing NPCs. They will follow the player and attack. 
If you can get behind them but still within their attack range, they might not turn around!

# Math functions used:
There is a bouncy ball inside the double ramp that uses Vector2.Reflect and surface normals for its DVD screensaver animation,
it also moves using Vector2.MoveTowards.

The player's ability to jump is computed with the dot profuct between the up vector and the surface normal where they're standing.

Enemies, when out of aggro range, will use quaternion.Lerp to look towards the player "menacingly".

The moving platform uses the Mathf.Sin function for its back-and-forth movement.
