using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController//: IEndTurnDisposable
{
    private BoardCreator _boardCreator;
    private BoardView _boardView;

public void OnEndTurnEvent()
{
    // 1
}

    public BoardController()
    {
        _boardCreator = new BoardCreator();
    }

    public void CreateBoard()
    {
        _boardView = _boardCreator.CreateBoard();
        _boardView.transform.position = Vector3.zero;
    }
    
    public Vector3 GetBoardSurfaceCenter()
    {
        return _boardView.SurfaceCenter.position;
    }
}
