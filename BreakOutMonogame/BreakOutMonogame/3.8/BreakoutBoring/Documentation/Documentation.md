\# **Documention for BreakoutBoring Modifications,** *Maximum Cross*



\## **Game** **Functionality** **Changes**

* Put autopaddle = true statement into effect only if game is in DEBUG. Also doubles ball speed if in DEBUG for testing purposes.

  * Added autopaddle toggle on S/Down key pressed in DEBUG



\## **Ball Changes**

* 



\## **Score Manager Changes**

* Player actually loses a life upon ball hitting the "floor" 

  * ScoreManager flags when lives are equal to 0; Game requests this info from it's ScoreManager score and handle end of game logic





**## Paddle Changes**

* Added WAS(D) functionality for improved user experience. (WASD is superior to arrow keys as everyone knows and agrees)
* 





NTS:

Modification Description : what you changed and why its better/funner

Structural Changes : what new classes did you add what classes did you modify. Why did you decide to make these changes where you did?

State Changes : Did you need to refactor any state to accommodate your changes? What did you refactor and why?

Maintainability : Did you improve the baseline of the source code to make it more sustainable or did you accrue technical debt

