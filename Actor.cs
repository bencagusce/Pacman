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
    protected float walkSpeed;
    protected float animationBuffer = 0;
    protected float keyFrameThreshold;
    protected bool keyframe = false;
    protected const float RADIUS = 9;
    protected Direction direction;

    protected readonly static Vector2f[] directionVectors =
    {
        new (0, -1),
        new (0, 1),
        new (1, 0),
        new (-1, 0)
    };
    public Actor(): base ("pacman")
    {
        
    }
}