using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    private BoardCreator _boardCreator;
    private BoardView _boardView;

    public BoardController()
    {
        _boardCreator = new BoardCreator();
    }

    public void CreateBoardView()
    {
        _boardView = _boardCreator.CreateBoard();
        _boardView.transform.position = Vector3.zero;
    }
    
    public Vector3 GetBoardSurfaceCenter()
    {
        return _boardView.SurfaceCenter.position;
    }
}
