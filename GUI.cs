using SFML.Graphics;
using SFML.System;

namespace Pacman;

public sealed class GUI : Entity
{
    Text scoreText = new Text();
    private int maxHealth = 3;
    private int currentHealth;
    private int currentScore = 0;
    public GUI() : base("pacman")
    {
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void Create(Scene scene)
    {
        Font font = scene.Assets.LoadFont("pixel-font");
        scoreText.Font = font;
        scoreText.DisplayedString = "Score";
        currentHealth = maxHealth;
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
            sprite.Position += new Vector2f(18, 0);
        }

        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(
            414 - scoreText.GetGlobalBounds().Width, 396
        );
        target.Draw(scoreText);
    }
}