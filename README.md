# Fall2023_CSC403_Project
https://docs.google.com/document/d/1zHnQpLB5wINaImM2AgnTISHE9KPizYZTDYcqs4fUyXs/edit


# CSC 403 Documentation

## Overview

### Menus

#### Main Menu
When the application starts, you are greeted with a main menu that has three buttons:

- **Start:** Starts the game and sends you to a character selection screen.
- **Controls:** Displays a popup window that outlines the controller interface/button usage.
- **Quit:** Closes the application.

#### Character Selection
When this window opens, you are greeted by three sprites. These sprites do not change the player’s stats; it is purely cosmetic. After selecting a sprite, the player will be loaded into the start of the game.

#### Pause Menu
At any time during gameplay, you may hit the spacebar which will open a window with two button options:

- **Resume:** Simply go back to the game.
- **Quit:** End application.

### Battle

#### Light Attack
Randomly generates an attack value (1-3) that gets multiplied by the player's strength for an attack. This costs 1 energy.

#### Heavy Attack
Randomly generates an attack value (3-5) that gets multiplied by the player's strength for an attack. This costs 2 energy.

#### Heal
Randomly generates a healing value (1-4) and multiplies it by player level. That value gets added to player health. You cannot heal over max health. This costs 1 energy.

#### Flee
Escape from battle. Closes the battle window. This does not cost energy.

#### Energy
While in battle, certain actions cost energy. The total amount of energy is shown at the top of the window and starts at 2. This allows a player to do multiple actions in a single turn of combat as long as the energy allows. After all energy has been spent, the enemy will attack.

- **2 Energy Actions:** Heavy Attack
- **1 Energy Actions:** Light Attack, Heal
- **Fleeing:** Does not cost any energy.

### Experience

#### Functions

- **LevelUp:** Gives +4 to the maximum health stat and can be uncommented to also grant extra strength and health upon a level up, grants the player full health after leveling up.
- **AddXP:** Defeating enemies and grabbing the XP item in the game will call the AddXP function which will then be used to check if the player has enough XP to level up, and if so, the LevelUp function is called.
- **xpItem:** An item in the game that instantly gives you a level up.

### Sound

#### SoundPlayer

- **Soundtrack:** Plays the game soundtrack when the player is not in a battle and resumes when they return to the level after a battle.

#### Action Sounds

Sounds are played when the user attacks the enemy or when the user dies in combat.

### Death

#### Player Death - playerDeath()
In FrmBattle.cs, added a function playerDeath() that pops up a full-screen Death Screen, giving the player two button options to either close the game or restart it. This Death Screen is designed and coded in DeathScreen.cs, DeathScreen.Designer.cs, and DeathScreen.resx. Button functionality for both of the buttons is in DeathScreen.cs.

- **closeButton()**
- **restartButton()**

#### Enemy Death - enemyDeath()
In FrmBattle.cs, added a function called enemyDeath() that nullifies the instance of the enemy and moves the collider to an unreachable part of the screen. enemy.Collider.MovePosition(0,0); enemyDeath() is called in UpdateHealthBars() when enemy health value is checked. Image of the enemy still remains; plan for the future is to change the implementation of enemy.Img to be a PictureBox instance and edit all enemy images through the instance’s declared PictureBox.

### New Design Options

- New character and enemy designs for multiple levels.
  - Boss Level
  - Level 2
  - Level 1

#### New Character Options

#### New Enemy Options

### Levels

- New levels and designs.

### Lives

#### Player Lives

Player has 2 lives to start the game. When their health reaches zero, a life is used and health is changed to MaxHealth.

### Controller

Can be played with a USB SNES controller when the solution is run with the dependency packages installed. The controller can be used for movement during gameplay. This was included using the SharpDX and SharpDX.DirectInput Libraries so that the controller inputs could be interpreted by the machine. The SharpDX references are in FrmLevel where the controller inputs are handled whether they are button pressed or a direction on the DPad.
