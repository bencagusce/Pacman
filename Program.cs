using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Pacman;

class Program 
{
    public static Direction Direction { get; private set; }
    static void Main(string[] args) 
    {
        using (var window = new RenderWindow(
        new VideoMode(828, 900), "Pacman")) 
        {
            window.Closed += (o, e) => window.Close();
            // Initialize
            Clock clock = new Clock();

            Scene scene = new Scene();
            scene.Loader.Load("maze");
            
            window.SetView(new View(new FloatRect(18, 0, 414, 450)));
            
            window.KeyPressed += (s, e) =>
            {
                switch (e.Code)
                {
                    case Keyboard.Key.Up:
                        Direction = Direction.UP;
                        break;
                    case Keyboard.Key.Down:
                        Direction = Direction.DOWN;
                        break;
                    case Keyboard.Key.Right:
                        Direction = Direction.RIGHT;
                        break;
                    case Keyboard.Key.Left:
                        Direction = Direction.LEFT;
                        break;
                }
            };
            
            while (window.IsOpen) 
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f);
                
                // Updates
                scene.UpdateAll(deltaTime);
                
                // Drawing
                window.Clear(Color.Black);
                scene.RenderAll(window);
                
                window.Display();
            }
        }
    }
}