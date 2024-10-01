using System.Text;

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

        string file = "maze.txt";
        List<string> lines = File.ReadLines(file, Encoding.UTF8).ToList();
        
        for (int y = 0; y < lines.Count(); y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                Entity entity;
                Create(line[x], out entity);
                
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
}