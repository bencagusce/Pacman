# The game needs to contain:
## 
- [x] Player character.
    - [x] Can be moved by player.
    - [x] Left, Right, Up and Down.
    - [x] Can’t move through walls.
    - [x] Loops around when going outside the edge of the screen.
- [x] Ghosts.
    - [x] Moves through the maze at random.
    - [x] Can’t move through walls.
    - [x] Can be turned between enemy and prey state.
    - [x] Has a different graphical representation for each state.
    - [x] Is ”killed” when colliding with pacman as prey, but not enemy.
    - [x] Is reset to start position when killed.
- [x] Coins & Score.
    - [x] Player gains points when pacman collides with coins.
    - [x] When the last coin is eaten the level should reset, but the score and health should remain.
- [x] Candy.
    - [x] Pacman can ”eat” candy.
    - [x] Eating candy turns the ghosts from enemies to prey, for a set amount of time, then they turn back to enemies.
- [x] Health.
    - [x] Player lose health when colliding with enemy ghosts.
    - [x] Losing health should reset player to starting position.
    - [x] When health is 0, the game restarts completely.
- [x] Level.
    - [x] Walls that neither the player or the ghosts can move through.
    - [x] Is loaded from a text-file.
- [x] All of the above game elements has a functional graphical representation.
## At least one of the Bonus Features:
- [x] Animate the ghosts, and have pacman face in the movement direction.
- [x] When ghosts and player is reset to start position, make them invulnerable and unable to move for a set amount of time.
- [x] Add a highscore that is shown when the game is lost (health = 0) and is saved between sessions (written to a file).

## FIX IF TIME
- [ ] Pacman 180s
- [ ] Ghosts 180n't
- [ ] Pathfinding ghosts
- [ ] Connected textures