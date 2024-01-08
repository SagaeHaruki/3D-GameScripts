### 🔨 3D GameScripts:
3D Game using Unity Engine
---

<h1>Dev's Note:</h1>

- If Some Objects can be added here then i will also publicly allow anyone to use it.
- This is using a Unity Editor version of (2021.3.23f1).

<h1>Future plans:</h1>

- Attacking Script
- NPC Interaction Script
- Inventory System Script (Prolly max of 9 or 10 Slots).
- Menu / Pause Game Script
- Loading Screen Script
- Stamina and Oxygen System (Stamina System is already done, i haven't re-implemented it yet) (Oxygen System will be same to Stamina system, but only works when the player is swimming).
- Minimap Script (This script is already done, i haven't re-implemented it yet).
- Enemy Attack & Player Detection Script

<h1>Current Changes: [January 06, 2024]</h1>

> What's New?: "Added a Swimming State"


> IK Script:
- IK System will not work when the player is currently swimming.
- Explanation: apparently the ik system magnets the player to the ground. 
  
> Movement Script:
- Added a few variables for the newly added "Swimming State'.

> Falling State Script:
- Fixed when the player is currently falling, and the is swimming tirgger, isFalling state will be turned off.

> Animations Script:
- Added 2 new booleans for the Swimming, isSwimIdle and isSwimming.
- Since there will be 2 new idle states (When grounded and When swimming).
- Edit: 2 new animations were introduced (Water threading = "Idle" and the Swmming = "Moving Swim").

> Change State Script:
- Introduced 4 types of swimming States (Swim Idle, Slow Swim, Fast Swim, Speed Swim).
- Edit: might only do (Swim Dile, Slow Swim, and Fast Swim).
- Explanation: this is due to limited amount of free animations i've got.

> Grounded State Script:
- Player will have no gravity when swimming. (Prolly not the right way to say it)
- Edit: Might revert this in exchange for invisible layer that the player can swim through.
- Explanation: Some animation are glitching upon using no gravity.

> Jumping State Script:
- Jump Cooldown adjusted from (1.5f to 1f). [Apparenly 1.5f is too long]
- Fix the jump when walking. [This doesn't work before]

> Movement Change Script:
- Player will be unable to dash when swimming.

> Swimming Script (New):
- Note: This Script will be added to a (Mainly a cube), with on Trigger effect.
- The cube will be positioned in a right height, for better trigger

