using System.Text;
using SFML.System;

namespace Pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private string currentScene = "", nextScene = "";
    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>
        {
            { '#', () => new Wall() },
            { '.', () => new Coin() },
            { 'g', () => new Ghost() },
            { 'c', () => new Candy()},
            { 'p', () => new Pacman()}
            // { '|', () => new }
        };
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        scene.Clear();

        string file = $"assets/{nextScene}.txt";
        List<string> lines = File.ReadLines(file, Encoding.UTF8).ToList();
        
        for (int y = 0; y < lines.Count(); y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#') scene.SpawnWall(x,y);
                
                Entity entity;
                if (Create(line[x], out entity))
                {
                    entity.Position = new Vector2f(  x * 18 + 9, y * 18 + 9);
                    scene.Spawn(entity);
                }
            }
        }
        
        currentScene = nextScene;
        nextScene = "";
    }
    private bool Create(char symbol, out Entity created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity> loader))
        {
            created = loader();
            return true;
        }
        created = null;
        return false;
    }

    public void Load(string scene) => nextScene = scene;

    public void Reload() => nextScene = currentScene;
}