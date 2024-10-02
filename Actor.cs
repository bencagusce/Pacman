using SFML.System;

namespace Pacman;

public enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}
public class Actor : Entity
{
    protected int animationFrame;
    protected float walkSpeed = 100f;
    protected float animationBuffer = 0;
    protected float keyFrameThreshold;
    protected bool keyframe = false;
    protected const float RADIUS = 9;
    protected Direction direction;
    protected float originalSpeed;
    protected Vector2f originalPosition;

    protected readonly static Vector2f[] directionVectors =
    {
        new (0, -1),
        new (0, 1),
        new (1, 0),
        new (-1, 0)
    };

    public Actor() : base("pacman"){}

    protected void Reset()
    { 
        Position = originalPosition;
        walkSpeed = originalSpeed;

    }

    protected void ResetPoint()
    {
        originalPosition = Position;
        originalSpeed = walkSpeed;
        Console.WriteLine($"originalSpeed{originalSpeed}, original Position{originalPosition}");
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        ResetPoint();
        Reset();
        
    }

    /// <summary>
    /// Divides a vector's axis by 18 and rounds them to integers 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    protected static Vector2i RoundToGrid(Vector2f v)
    {
        return new Vector2i((int)MathF.Round(v.X / 18), (int)MathF.Round(v.Y / 18));                                       
    }

    protected bool PassedTileCenter(Vector2f newPos, Vector2f oldPos)
    {
        float n = newPos.X * directionVectors[(int)direction].X +
                  newPos.Y * directionVectors[(int)direction].Y;
        
        float o = oldPos.X * directionVectors[(int)direction].X +
                  oldPos.Y * directionVectors[(int)direction].Y;

        return n % 18 < o % 18;
    }
}