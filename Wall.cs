using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class Wall : Entity
{
    public Wall() : base("pacman")
    {
        
    }

    public override bool Solid => true;
    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
    }
    public override void Update(Scene scene, float deltaTime) {}
}