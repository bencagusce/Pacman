# The game needs to contain:
## 
- [ ] Player character.
    - [x] Can be moved by player.
    - [x] Left, Right, Up and Down.
    - [x] Can’t move through walls.
    - [ ] Loops around when going outside the edge of the screen.
- [ ] Ghosts.
    - [ ] Moves through the maze at random.
    - [ ] Can’t move through walls.
    - [ ] Can be turned between enemy and prey state.
    - [ ] Has a different graphical representation for each state.
    - [ ] Is ”killed” when colliding with pacman as prey, but not enemy.
    - [ ] Is reset to start position when killed.
- [ ] Coins & Score.
    - [ ] Player gains points when pacman collides with coins.
    - [ ] When the last coin is eaten the level should reset, but the score and health should remain.
- [ ] Candy.
    - [ ] Pacman can ”eat” candy.
    - [ ] Eating candy turns the ghosts from enemies to prey, for a set amount of time, then they turn back to enemies.
- [ ] Health.
    - [ ] Player lose health when colliding with enemy ghosts.
    - [ ] Losing health should reset player to starting position.
    - [ ] When health is 0, the game restarts completely.
- [ ] Level.
    - [ ] Walls that neither the player or the ghosts can move through.
    - [x] Is loaded from a text-file.
- [x] All of the above game elements has a functional graphical representation.
## At least one of the Bonus Features:
- [ ] Animate the ghosts, and have pacman face in the movement direction.
- [ ] When ghosts and player is reset to start position, make them invulnerable and unable to move for a set amount of time.
- [ ] Add a highscore that is shown when the game is lost (health = 0) and is saved between sessions (written to a file).

## fix for next time
- [x] hearts to show on screen