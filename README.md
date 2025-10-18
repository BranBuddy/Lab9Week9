Observer Pattern: We made a game manager singleton class to start off. Then we made a function that will add a int supplied by the enemy's point value and add it to the player's score. Whenever the enemy dies, then the score is executed.
Builder Pattern: In the enemy class, the base attributes are defined for the enemy. Then in a nested class a function is defined to set those attributes to a set of parameter. Then a build function is called that returns the defined enemy. Then in their own respective classes,
this function is called and assigned different values depending on need
