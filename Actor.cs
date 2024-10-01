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
    protected Direction direction;
    public Actor(): base ("pacman")
    {
        
    }
}