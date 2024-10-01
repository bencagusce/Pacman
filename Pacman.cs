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
        keyFrameThreshold = 0.2f;
        animationFrame = 1;
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public void Update()
    {
        //check direction and set enum direction
        //check for corner turns
        //update sprite
    }
    //0,1,2,-1
    public void SpriteChange(float deltatime)
    {
        //change sprite based on an animationbuffer
        animationBuffer += deltatime;
        if ((animationBuffer > keyFrameThreshold))
        {
            animationFrame++;
            if (animationFrame == 3)
            {
                countDown = true;
                animationFrame++;
            }
            animationFrame = animationFrame % 3;
            
            switch (direction)
            {
                case Direction.UP:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesUp[animationFrame], new Vector2i(18, 18));
                    if (countDown) animationFrame = -1;
                    break;
                }
                case Direction.DOWN:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesDown[animationFrame], new Vector2i(18, 18));
                    if (countDown) animationFrame = -1;
                    break;
                }
                case Direction.RIGHT:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesRight[animationFrame], new Vector2i(18, 18));
                    if (countDown) animationFrame = -1;
                    break;
                }
                case Direction.LEFT:
                {
                    animationBuffer = 0;
                    sprite.TextureRect = new IntRect(spritesLeft[animationFrame], new Vector2i(18, 18));
                    if (countDown) animationFrame = -1;
                    break;
                }
            }
        }
    }
}