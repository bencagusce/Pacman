using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class GUI : Entity
{
    Text scoreText = new Text();
    private const int MAXHEALTH = 3;
    public static int currentHealth;
    public static int currentScore = 0;
    public static bool resetStats = true;

    public GUI() : base("pacman")
    {
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        sprite.Scale = new Vector2f(2, 2);
    }

    public override void Create(Scene scene)
    {
        Font font = scene.Assets.LoadFont("pixel-font");
        scoreText.Font = font;
        scoreText.DisplayedString = "Score";
        scoreText.CharacterSize = 30;
        scoreText.Scale = new Vector2f(0.75f, 0.75f);
        
        if (resetStats)
        {
            currentHealth = MAXHEALTH;
            currentScore = 0;
        }
        
        scene.LoseHealth += OnLoseHealth;
        scene.GainScore += OnGainScore;
        
        base.Create(scene);
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.LoseHealth -= OnLoseHealth;
        scene.GainScore -= OnGainScore;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            resetStats = true;
            scene.Loader.Reload();
        }
    }

    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
        if (!scene.FindByType<Coin>(out _))
        {
            resetStats = false;
            scene.Loader.Reload();
        }
    }
    
    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < MAXHEALTH; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18) //full heart
                : new IntRect(72, 0, 18, 18);//Empty heart
            base.Render(target);
            sprite.Position += new Vector2f(36, 0);
        }

        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(
            414 - scoreText.GetGlobalBounds().Width, 396
        );
        target.Draw(scoreText);
    }
}