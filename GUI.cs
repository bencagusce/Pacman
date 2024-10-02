using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class GUI : Entity
{
    Text scoreText = new Text();
    private int maxHealth = 3;
    private int currentHealth;
    public static int currentScore = 0;
    public GUI() : base("pacman")
    {
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
        sprite.Scale = new Vector2f(2, 2);
    }

    public override void Create(Scene scene)
    {
        currentHealth = maxHealth;
        Font font = scene.Assets.LoadFont("pixel-font");
        scoreText.Font = font;
        scoreText.DisplayedString = "Score";
        scoreText.CharacterSize = 30;
        scoreText.Scale = new Vector2f(0.75f, 0.75f);

        scene.LoseHealth += OnLoseHealth;
        scene.GainScore += OnGainScore;
        
        base.Create(scene);
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 3;
            scene.Loader.Reload();
        }
    }

    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
    }
    
    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < maxHealth; i++)
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