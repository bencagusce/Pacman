using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Coin : Entity
{
    public Coin() : base ("pacman")
    {
        sprite.TextureRect = new IntRect(36, 36, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }
    public override FloatRect Bounds
    { get 
        {
            var bounds = base.Bounds;
            bounds.Left += 3;
            bounds.Width -= 6;
            bounds.Top += 3;
            bounds.Height -= 6;
            return bounds;
        }
    }

    //public bool touching(Pacman pacman)
    //{
    //    
    //}
}