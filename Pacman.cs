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

    private RectangleShape debugBox;
    public Pacman() : base()
    {
        walkSpeed = 10.0f;
        keyFrameThreshold = 0.2f;
        animationFrame = 1;
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        direction = Direction.RIGHT;
        debugBox = new RectangleShape();
        debugBox.Size = new Vector2f(18, 18);
        debugBox.Origin = new Vector2f(9, 9);
        debugBox.OutlineColor = Color.Red;
        debugBox.OutlineThickness = 2;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        SpriteChange(deltaTime);
        
        Vector2f oldPosition = Position - sprite.Origin;

        Position += walkSpeed * deltaTime * directionVectors[(int)direction];

        Vector2f newPosition = Position - sprite.Origin;

        Vector2i intPosition = RoundToGrid(newPosition);
        debugBox.Position = sprite.Origin + (Vector2f)(18 * intPosition);
        
        Vector2i frontPosition = RoundToGrid(newPosition + RADIUS * directionVectors[(int)direction]);

        bool hitAWall = false;
        if (scene.walls[frontPosition.X, frontPosition.Y])
        {
            Position = sprite.Origin + (Vector2f)(18 * intPosition);
            hitAWall = true;
        }
        
        switch (direction)
        {
            case Direction.RIGHT:
            {
                if (newPosition.X % 18 < oldPosition.X % 18 || hitAWall)
                {
                    if (Program.Direction == Direction.UP &&
                        !scene.walls[intPosition.X, intPosition.Y - 1])
                    {
                        direction = Direction.UP;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                    
                    if (Program.Direction == Direction.DOWN &&
                        !scene.walls[intPosition.X, intPosition.Y + 1])
                    {
                        direction = Direction.DOWN;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                }
                break;
            }
            case Direction.LEFT:
            {
                if (-newPosition.X % 18 > -oldPosition.X % 18 || hitAWall)
                {
                    if (Program.Direction == Direction.UP &&
                        !scene.walls[intPosition.X, intPosition.Y - 1])
                    {
                        direction = Direction.UP;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                    
                    if (Program.Direction == Direction.DOWN &&
                        !scene.walls[intPosition.X, intPosition.Y + 1])
                    {
                        direction = Direction.DOWN;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                }
                break;
            }
            case Direction.DOWN:
            {
                if (newPosition.Y % 18 < oldPosition.Y % 18 || hitAWall)
                {
                    if (Program.Direction == Direction.LEFT &&
                        !scene.walls[intPosition.X -1, intPosition.Y])
                    {
                        direction = Direction.LEFT;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                    
                    if (Program.Direction == Direction.RIGHT &&
                        !scene.walls[intPosition.X + 1, intPosition.Y])
                    {
                        direction = Direction.RIGHT;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                }
                break;
            }
            case Direction.UP:
            {
                if (-newPosition.Y % 18 > -oldPosition.Y % 18 || hitAWall)
                {
                    if (Program.Direction == Direction.LEFT &&
                        !scene.walls[intPosition.X -1, intPosition.Y])
                    {
                        direction = Direction.LEFT;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                    
                    if (Program.Direction == Direction.RIGHT &&
                        !scene.walls[intPosition.X + 1, intPosition.Y])
                    {
                        direction = Direction.RIGHT;
                        Position = sprite.Origin + (Vector2f)(18 * intPosition);
                    }
                }
                break;
            }
        }
    }
    
    public override void Render(RenderTarget target)
    {
        target.Draw(debugBox);
        target.Draw(sprite);
    }

    public Vector2i RoundToGrid(Vector2f v)
    {
        return new Vector2i((int)MathF.Round(v.X / 18), (int)MathF.Round(v.Y / 18));                                       
    }
    
    //0,1,2,-1
    public void SpriteChange(float deltaTime)
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
}