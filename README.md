### 🔨 3D GameScripts:
3D Game using Unity Engine
---

<h1>Dev's Note:</h1>

- If Some Objects can be added here then i will also publicly allow anyone to use it.
- This is using a Unity Editor version of (2021.3.23f1).

<h1>Future plans:</h1>

- Attacking Script. [DONE]
- NPC Interaction Script.
- Inventory System Script (Prolly max of 9 or 10 Slots).
- Menu / Pause Game Script.
- Loading Screen Script.
- Stamina and Oxygen System. [DONE]
- Minimap Script (This script is already done, i haven't re-implemented it yet).
- Enemy Attack & Player Detection Script.

<h1>Current Changes: [January 10, 2024]</h1>

> What's New?: "Attacking System"
- It's Finally here! Attacking Script
- 3 New Animations for the Attacking Script

> IK Script:
- No Changes
  
> Movement Script:
- Added a few Variables for the attacking Script (Including Cooldowns).
- Adjustments to keep the player state "isMoving" even when attacking.

> Falling State Script:
- No Changes

> Animations Script:
- Adjustments for smoother animation when Moving and is trying to attack.
- Adjustments for smoother animation when Not Moving and is tryign to attack.
- Stop some animation when the player is attacking.

> Change State Script:
- Suppose to have a Attacking State. (Will do no next commit)

> Grounded State Script:
- Fixed the "No Gravity" when player is currently Swimming.

> Jumping State Script:
- Player will not be able to Jump when currently attacking.

> Movement Change Script:
- Player will not be able to Dash when currently attacking.
- When the player is currently Airborned, Swimming or Jumping it cannot attack.

> Swimming Script:
- No Changes

> Camera Zoom Script (New):
- No Changes

> Stamina System Script (New):
- No Changes

> Attacking Script (New):
- Added 3 Attack Types
- Each attack has cooldowns (To smoothen the animation)
- When not moving animation has exit time.
- When moving animation can exit faster.
