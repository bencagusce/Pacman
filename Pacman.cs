using System.ComponentModel.Design.Serialization;
using System.Data;
using SFML.Graphics;
using SFML.System;

namespace Pacman;


public sealed class Pacman : Actor
{
    private bool countDown;
    private static Vector2i[] spritesUp =
    {
        new(18, 18),
        new(0, 18),
        new(36, 54)
    };
    private static Vector2i[] spritesDown =
    {
        new(18, 54),
        new(0, 54),
        new(36, 54)
    };
    private static Vector2i[] spritesRight =
    {
        new(18, 0),
        new(0, 0),
        new(36, 54)
    };
    private static Vector2i[] spritesLeft =
    {
        new(18, 36),
        new(0, 36),
        new(36, 54)
    };

    public Pacman() : base()
    {
        walkSpeed = 100.0f;
        keyFrameThreshold = 0.08f;
        animationFrame = 1;
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        direction = Direction.RIGHT;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        scene.LoseHealth += OnLoseHealth;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }

    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    // step 45 after fixing reset function.
    public override void Update(Scene scene, float deltaTime)
    {
        SpriteChange(deltaTime);
        
        // MOVEMENT
        // Position before movement
        Vector2f oldPosition = Position - sprite.Origin;
        // Move
        Position += walkSpeed * deltaTime * directionVectors[(int)direction];
        // Position after movement
        Vector2f newPosition = Position - sprite.Origin;
        // Pacman's position on the wall grid
        Vector2i intPosition = RoundToGrid(newPosition);
        // The position of the front of pacman on the wall grid
        Vector2i frontPosition = RoundToGrid(newPosition + RADIUS * directionVectors[(int)direction]);

        // Check if pacman has hit a wall
        bool hitAWall = false;
        if (scene.walls[frontPosition.X, frontPosition.Y])
        {
            Position = sprite.Origin + (Vector2f)(18 * intPosition);
            hitAWall = true;
        }

        // If pacman has just passed the center of a tile or has hit a wall
        if (PassedTileCenter(newPosition, oldPosition) || hitAWall)
        {
            // Check that player input direction is not the same as pacman's heading or the opposite direction
            if (Program.Direction != direction && Program.Direction != direction + 1 - 2 * ((int)direction % 2))
            {
                // Check that there is no wall in the desired travel direction
                if (!scene.walls[intPosition.X + (int)directionVectors[(int)Program.Direction].X,
                        intPosition.Y + (int)directionVectors[(int)Program.Direction].Y])
                {
                    // Change direction to reflect input
                    direction = Program.Direction;
                    
                    // Move pacman to the center of the current tile
                    Position = sprite.Origin + (Vector2f)(18 * intPosition);
                }
            }
        }
    }
    
    //0,1,2,-1
    private void SpriteChange(float deltaTime)
    {
        //change sprite based on an animationbuffer
        animationBuffer += deltaTime;
        if ((animationBuffer > keyFrameThreshold))
        {
            animationFrame++;
            if (animationFrame == 3)
            {
                countDown = true;
                animationFrame++;
            }
            animationFrame %= 3;
            
            switch (direction)
            {
                case Direction.UP:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesUp[animationFrame], new Vector2i(18, 18));
                    if (countDown)
                    {
                        animationFrame = -1;
                        countDown = false;
                    }
                    break;
                }
                case Direction.DOWN:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesDown[animationFrame], new Vector2i(18, 18));
                    if (countDown)
                    {
                        animationFrame = -1;
                        countDown = false;
                    }
                    break;
                }
                case Direction.RIGHT:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesRight[animationFrame], new Vector2i(18, 18));
                    if (countDown)
                    {
                        animationFrame = -1;
                        countDown = false;
                    }
                    break;
                }
                case Direction.LEFT:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesLeft[animationFrame], new Vector2i(18, 18));
                    if (countDown)
                    {
                        animationFrame = -1;
                        countDown = false;
                    }
                    break;
                }
            }
        }
    }
    
    public override void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}