using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Ghost : Actor
{
    private bool isBlue;
    private bool firstSprite = true;
    private Vector2i spritePosition = new Vector2i(36, 0);
    private static Random rng = new Random();
    private float preyTime = 0;
    private const float PREYSPEED = 60f;
    public Ghost()
    {
        keyFrameThreshold = 0.25f;
        sprite.TextureRect = new IntRect(36, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        direction = Direction.UP;
    }

    public override void Create(Scene scene)
    {
        scene.CandyEaten += OnCandyEaten;
        base.Create(scene);
    }

    public override void Destroy(Scene scene)
    {
        scene.CandyEaten -= OnCandyEaten;
        base.Destroy(scene);
    }

    private void OnCandyEaten(float time)
    {
        preyTime = time;
        walkSpeed = PREYSPEED;
        spritePosition = new Vector2i(spritePosition.X, 18);
        sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
    }

    protected override void Reset()
    {
        base.Reset();
        preyTime = 0;
        walkSpeed = DEFAULTWALKSPEED;
        spritePosition = new Vector2i(spritePosition.X, 0);
        sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (preyTime > 0)
        {
            preyTime -= deltaTime;
            if (preyTime < 0)
            {
                walkSpeed = DEFAULTWALKSPEED;
                spritePosition = new Vector2i(spritePosition.X, 0);
                sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
            }
        }
        SpriteChange(deltaTime);
        
        // MOVEMENT
        // Position before movement
        Vector2f oldPosition = Position - sprite.Origin;
        // Move
        Position += walkSpeed * deltaTime * directionVectors[(int)direction];
        // Loop if outside the map
        if (Position.X < 0) Position = new Vector2f(450,Position.Y);
        if (Position.X > 450) Position = new Vector2f(0,Position.Y);
        // Position after movement
        Vector2f newPosition = Position - sprite.Origin;
        // Ghost's position on the wall grid
        Vector2i intPosition = RoundToGrid(newPosition);
        // The position of the front of ghost on the wall grid
        Vector2i frontPosition = RoundToGrid(newPosition + RADIUS * directionVectors[(int)direction]);

        // Check if ghost has hit a wall
        bool hitAWall = false;
        if (scene.walls[frontPosition.X + 1, frontPosition.Y + 1])
        {
            Position = sprite.Origin + (Vector2f)(18 * intPosition);
            hitAWall = true;
        }

        // If ghost has just passed the center of a tile or has hit a wall
        if (PassedTileCenter(newPosition, oldPosition) || hitAWall)
        {
            List<Direction> possibleMovements = new List<Direction>();
            for (int i = 0; i < 4; i++)
            {
                
                if (!scene.walls[intPosition.X + 1 + (int)directionVectors[i].X,
                        intPosition.Y + 1 + (int)directionVectors[i].Y] &&
                        i != (int)direction + 1 - 2 * ((int)direction % 2))
                {
                    possibleMovements.Add((Direction)i);
                }
            }

            Direction oldDirection = direction;
            
            // Change direction to reflect input
            direction = possibleMovements[rng.Next(possibleMovements.Count)];
                    
            // If ghost has turned move ghost to the center of the current tile
            if (oldDirection != direction) Position = sprite.Origin + (Vector2f)(18 * intPosition);
        }
        base.Update(scene, deltaTime);
    }
    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacman)
        {
            if (preyTime > 0)
            {
                scene.PublishGainScore(200);
                Reset();
            }
            else
            {
                scene.PublishLoseHealth(1);
                Reset();
            }
        }
    }
    private void SpriteChange(float deltaTime)
    {
        animationBuffer += deltaTime;
        if (animationBuffer > keyFrameThreshold)
        {
            animationBuffer = 0;  
            spritePosition += firstSprite ? new Vector2i(18, 0) : new Vector2i(-18, 0);
            sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
            firstSprite = !firstSprite;
        }
    }
}