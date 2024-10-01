using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Candy : Entity
{
    public Candy() : base("pacman")
    {
        sprite.TextureRect = new IntRect(54, 36, 24, 24);
        sprite.Origin = new Vector2f(9, 9);
    }
}