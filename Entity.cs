using SFML.Graphics;
using SFML.System;

namespace Pacman;
public class Entity
{
    private string textureName;
    protected Sprite sprite = new Sprite();
    public bool Dead;
    protected Entity(string textureName)
    {
        this.textureName = textureName;
        sprite = new Sprite();
    }
    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }
    public FloatRect Bounds => sprite.GetGlobalBounds();
    public virtual bool Solid => false;
    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(textureName);
    }
    public virtual void Destroy(Scene scene)
    {
        
    }
    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds)) CollideWith(scene, found);
    }
    protected virtual void CollideWith(Scene s, Entity other)
    {
        // Empty ->Overriden by implementing classes
    }
    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}