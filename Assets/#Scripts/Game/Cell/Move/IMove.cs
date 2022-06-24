using System;

public interface IMove
{
    void Move();
    
    Action MoveCallback { get; set; }
}
