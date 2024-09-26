using SFML.Graphics;
using System.Collections.Generic;
namespace Pacman;

class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;
    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    }
    public Texture LoadTexture(string name) 
    {
        if (textures.TryGetValue(name, out Texture found)) return found;
        string fileName = $"assets/{name}.png";
        Texture texture = new Texture(fileName);
        textures.Add(name, texture);
        return texture;
    }
    public Font LoadFont(string name)
    {
        //ska inte stå massa "texture" utan "font" 5
        if (textures.TryGetValue(name, out Texture found)) return found;
        string fileName = $"assets/{name}.ttf";
        Texture texture = new Texture(fileName);
        textures.Add(name, texture);
        return texture;
    }
}