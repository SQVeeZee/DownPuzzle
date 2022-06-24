using System;
using System.Collections.Generic;
using System.Linq;

public class CellsMoveController
{
    public event Action onMoveCompleted = default;

    private List<IMove> _moves = new List<IMove>();

    public void Initialize(GridCell[,] gridCells, MovePattern movePattern)
    {
        _moves = DefineMoveLogic(gridCells, movePattern);

        RegisterMoveLogic(_moves);
    }

    public void MoveCells()
    {
        if (_moves.Count == 0)
        {
            OnCompleteMove();
        }
        else
        {
            _moves[0].Move();
        }
    }

    private List<IMove> DefineMoveLogic(GridCell[,] gridCells, MovePattern movePattern)
    {
        List<IMove> moves = new List<IMove>();

        if (movePattern.HorizontalMovePattern.HorizontalDirectionType != EHorizontalDirectionType.NONE)
            moves.Add(new HorizontalMove(gridCells));

        if (movePattern.VerticalMovePattern.VerticalDirectionType != EVerticalDirectionType.NONE)
            moves.Add(new VerticalMove(gridCells));

        return moves;
    }

    private void RegisterMoveLogic(List<IMove> moves)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (i < moves.Count - 1)
            {
                moves[i].MoveCallback = moves[i + 1].Move;
            }
            else
            {
                moves[i].MoveCallback = OnCompleteMove;
            }
        }
    }

    private void OnCompleteMove() => onMoveCompleted?.Invoke();
}