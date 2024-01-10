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

<h1>Current Changes: [January 09, 2024]</h1>

> What's New?: "Stamina Sytem"
- Stamina System Re-Implemented.
- Small Changes to some Scritps.

> IK Script:
- No Changes
  
> Movement Script:
- Added the required component - (Stamina System).
- Added some new variables to be used for Stamina System.

> Falling State Script:
- No Changes

> Animations Script:
- No Changes

> Change State Script:
- No Changes

> Grounded State Script:
- Fixed the "No Gravity" when player is currently Swimming.

> Jumping State Script:
- No Changes

> Movement Change Script:
- Player Cannot Dash when they are low on Stamina.
- Player Cannot Sprint on Low Stamina.
- Added a movement change when the player is on Swimming State.

> Swimming Script (New):
- Player will stop swimming when the oxygen is Running low

> Camera Zoom Script (New):
- Player can press a key (Default: LeftAlt) to show the cursor and stop the camera Movement.
- Player can now adjust their mouse sensitivity for the camera.

> Stamina System Script (New):
- Added a Stamina for the player.
- Added a Oxygen for the player.
- Player Regenerates their Stamina and Oxyen when running low
- Player will be prevented to do some movements that requires stamina/oxygen when they are running low on it
