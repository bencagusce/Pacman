namespace Pacman;

class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private string currentScene = "", nextScene = "";
    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>> 
        {
            {'#', () => new Wall()}
        }
        // Initialize dictionaty 6
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        scene.Clear();
        //TODO: Load scene file
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