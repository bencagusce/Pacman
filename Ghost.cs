using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Ghost : Actor
{
    private bool isBlue;
    private bool firstSprite;
    private Vector2i spritePosition = new Vector2i(36, 0);
    public Ghost() : base()
    {
        keyFrameThreshold = 0.25f;
        sprite.TextureRect = new IntRect(36, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        
    }

    public void Update(float deltaTime)
    {
        spritePosition = isBlue ? new Vector2i(spritePosition.X, 18) : new Vector2i(spritePosition.X, 0);
        sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
    }

    public void SpriteChange(float deltaTime)
    {
        animationBuffer += deltaTime;
        if (animationBuffer > keyFrameThreshold)
        {
            animationBuffer = 0;  
            spritePosition += firstSprite ? new Vector2i(18, 0) : new Vector2i(-18, 0);
            sprite.TextureRect = new IntRect(spritePosition, new Vector2i(18, 18));
        }

    }
}