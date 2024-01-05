### 🔨 3D GameScripts:
3D Game using Unity Engine
---

<h1>Dev's Note:</h1>

- If Some Objects can be added here then i will also publicly allow anyone to use it.
- This is using a Unity Editor version of (2021.3.23f1).

<h1>Future plans:</h1>

- Attacking Script
- NPC Interaction Script
- Inventory System Script (Prolly max of 9 or 10 Slots)
- Menu / Pause Game Script
- Loading Screen Script
- Minimap Script
- Enemy Attack & Player Detection Script

<h1>Current Changes: [January 05, 2024]</h1>

> IK Script
- Minor Adjustment
  
> Movement Script
- When Sprinting & Jumping, the distance of the jump is further than running.

> Falling State Script
- Adjust the falling delay when walking.
- Move the player when Falling, to avoid gliches.

> Animations Script
- Added the Dashing Bool for animation.
- Added the Idle Jumping animation.
- Adjusted some part upon adding the Dash Animation.

> Change State Script
- Added the Dash player state.

> Grounded State Script
- Adjusted the gravity pull to the ground from (-2f to -1.5f), for better falling pull.
- Player can now detect if going up or down a slope.

> Jumping State Script
- Jump Cooldown adjusted from (1.5f to 1f). [Apparenly 1.5f is too long]
- Fix the jump when walking. [This doesn't work before]

> Movement Change Script
- Added a Dash Motion Including the ff. (Cooldown, Duration, Distance, DashTime, and canDash).
- Change the sprinting method instead of holding it. Explanation: "When pressing the [LShift], you start by dashing, after a short while you will start sprinting, to stop sprinting, you must stop moving".
- Since you can now detect if going up or down a slope; movement change based on going up or down a slope, (Exept for the walking, it stays on current walking speed).
- Adjusted all the movement speed modifier, this is to sync with the Animation Speed.

