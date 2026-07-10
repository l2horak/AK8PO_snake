namespace SnakeClean.Core;

public sealed class Snake
{
    private readonly Queue<Position> _body = new();

    public Snake(Position head, Direction direction)
    {
        Head = head;
        Direction = direction;
    }

    public Position Head { get; private set; }
    public Direction Direction { get; private set; }
    public IReadOnlyCollection<Position> Body => _body.ToArray();

    public void ChangeDirection(Direction nextDirection)
    {
        if (IsOppositeDirection(nextDirection))
        {
            return;
        }

        Direction = nextDirection;
    }

    public void Move(bool grow)
    {
        _body.Enqueue(Head);
        Head = Head.Move(Direction);

        if (!grow && _body.Count > 0)
        {
            _body.Dequeue();
        }
    }

    public bool HitsItself()
    {
        return _body.Contains(Head);
    }

    public int Length => _body.Count;

    private bool IsOppositeDirection(Direction nextDirection)
    {
        return (Direction, nextDirection) switch
        {
            (Direction.Up, Direction.Down) => true,
            (Direction.Down, Direction.Up) => true,
            (Direction.Left, Direction.Right) => true,
            (Direction.Right, Direction.Left) => true,
            _ => false
        };
    }
}