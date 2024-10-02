using SFML.Graphics;
using SFML.System;

namespace Pacman;
public class Entity
{
    private string textureName;
    protected Sprite sprite = new Sprite();
    public bool Dead;
    public bool dontDestroyOnLoad;
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
    public virtual FloatRect Bounds => sprite.GetGlobalBounds();
    public virtual bool Solid => false;
    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(textureName);
    }
    public virtual void Destroy(Scene scene)
    {
        Dead = true;
    }
    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds)) CollideWith(scene, found);
    }
    protected virtual void CollideWith(Scene s, Entity other)
    {
        // Empty ->Overriden by implementing classes
        //Console.WriteLine("oignsegon");
    }
    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}