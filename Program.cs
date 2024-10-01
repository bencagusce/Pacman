﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Pacman;

class Program 
{
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
            
            while (window.IsOpen) 
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f);
                
                // Updates
                scene.UpdateAll(deltaTime);
                
                // Drawing
                window.Clear(new Color(223, 246, 245));
                scene.RenderAll(window);
                
                window.Display();
            }
        }
    }
}