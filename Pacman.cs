using System.Data;
using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Pacman : Actor
{
    //private override const float WALKSPEED = 100.0f;
    public Pacman() : base()
    {
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        WALKSPEED = 100.0f;
    }

    public void Update()
    {
        
    }
}