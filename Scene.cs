using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Pacman;

public class Scene
{
    //idk what i've written 7
    private List<Entity> entities;
    //public readonly SceneLoader.loader //idk if this is correct 7
    // public Texture LoadTexture(string a)
    // {
    //    return AssetManager.LoadTexture(a);
    // }
    
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }
    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i]; //something is wrong with the color of Entity 10
            entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }
    public void UpdateAll(float deltaTime)
    {
        HandleSceneChange();
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
        }

        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
            if (entity.Dead) entities.RemoveAt(i);
            else i++;
        }
    } 
        public void RenderAll(RenderTarget target)
    {
        foreach (var entity in entities)
        {
            entity.Render(target);
        }
    }
     public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (var entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
    }
    public IEnumerable<Entity> FindIntersects(FloatRect bounds) 
    {
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue; //also a problem with coor of entity.Dead 11
            if (entity.Bounds.Intersects(bounds)) yield return entity;
        }
    }
}   