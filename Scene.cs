using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Pacman;

public delegate void ValueChangedEvent(Scene scene, int value);

public delegate void TimeChangedEvent(float time);
public sealed class Scene
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event TimeChangedEvent CandyEaten;
    public readonly bool[,] walls;
    public readonly SceneLoader Loader = new SceneLoader();
    public readonly AssetManager Assets = new AssetManager();
    private List<Entity> entities;
    private const float GRACELENGTH = 3f;
    private float grace = GRACELENGTH;
    private int scoreGained;
    private int healthLost;
    private float candyTime;

    public Scene()
    {
        entities = new List<Entity>();
        walls = new bool[27,23];
    }
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }
    public void SpawnWall(int x, int y)
    {
        walls[x, y] = true;
    }
    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }
    public void UpdateAll(float deltaTime)
    {
        Loader.HandleSceneLoad(this);

        if (grace > 0)
        {
            grace -= deltaTime;
            return;
        }
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
        }

        if (scoreGained != 0)
        {
            GainScore?.Invoke(this, scoreGained);
            scoreGained = 0;
        }
        if (healthLost != 0)
        {
            LoseHealth?.Invoke(this, healthLost);
            healthLost = 0;
        }

        if (candyTime != 0)
        {
            CandyEaten?.Invoke(candyTime);
            candyTime = 0;
        }
        
        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
            if (entity.Dead) entities.RemoveAt(i);
            else i++;
        }
    }

    public void PublishGainScore(int amount)
        => scoreGained += amount;

    public void PublishLoseHealth(int amount)
        => healthLost += amount;

    public void PublishCandyEaten(float time)
        => candyTime = time;
    public void RenderAll(RenderTarget target)
    {
        foreach (var entity in entities)
        {
            entity.Render(target);
        }
    }

    public void StartGrace() => grace = GRACELENGTH;
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

        found = default;
        return false;
    }
    public IEnumerable<Entity> FindIntersects(FloatRect bounds) 
    {
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (bounds.Contains(entity.Position.X, entity.Position.Y)) yield return entity;
        }
    }
}   