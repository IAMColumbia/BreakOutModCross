\# **Documention for BreakoutBoring Modifications,** *Maximum Cross*



\## **Game** **Functionality** **Changes**

* Put autopaddle = true statement into effect only if game is in DEBUG. Also doubles ball speed if in DEBUG for testing purposes.

  * Added autopaddle toggle on S/Down key pressed in DEBUG
* Handle Game Over and next level

  * Both offer Enter to continue or Escape to exit
  * When blocks are all clear, new screen pops up and Enter increases block height by 1 or exit ends the game

    * Also displays score (cumulative over levels)
  * When lives are 0, new screen pops up where enter completely resets the game and exit ends the game

    * This is better because the game can continue to be played and player can chase a higher score
  * Modified ScoreManager to recognize when lives are 0 to flag Game1

    * ScoreManager already handles lives so i should continue to let it do that

      * This required an instance of the Game1 to call it's pop-up-screen, then I removed the lazy load in blockmanager to not modify it too much so this is technical debt
    * Game1 should handle the logic of the game itself resetting or continuing
  * Modified BlockManager to recognize next level and update to increase blocks height

    * BlockManager handles blocks



\## **Ball Changes**

* Bug to fix: - Can multihit blocks, instantly deleting them-i think echo handled this with a cooldown/invincibility frames
* Ball should reset on next level (it can currently go straight through on new level and end up on top of screen)
* Bugs fixed! Modified UpdateCheckBlocksForCollision to stop checking for collision after hitting a block.
* Ball resets on next level. Game1 requests Ball to reset it's position on game reset or next level reached.



\## **Score Manager Changes**

* Player actually loses a life upon ball hitting the "floor"

  * ScoreManager flags when lives are equal to 0; Game requests this info from it's ScoreManager score and then Game1 handles end of game logic





**## Paddle Changes**

* Added WAS(D) functionality for improved user experience. (WASD is superior to arrow keys as everyone knows and agrees)
* 





NTS:

Modification Description : what you changed and why its better/funner

Structural Changes : what new classes did you add what classes did you modify. Why did you decide to make these changes where you did?

State Changes : Did you need to refactor any state to accommodate your changes? What did you refactor and why?

Maintainability : Did you improve the baseline of the source code to make it more sustainable or did you accrue technical debt

