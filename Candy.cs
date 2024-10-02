using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Candy : Entity
{
    public Candy() : base("pacman")
    {
        sprite.TextureRect = new IntRect(54, 36, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }
    public override FloatRect Bounds
    { get 
        {
            var bounds = base.Bounds;
            bounds.Left += 0;
            bounds.Width -= 0;
            bounds.Top += 3;
            bounds.Height -= 5;
            return bounds;
        }
    }
}