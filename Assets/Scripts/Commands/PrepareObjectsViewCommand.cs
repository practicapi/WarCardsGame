
using UnityEngine;

public class PrepareObjectsViewCommand : BaseCommand
{
    private Vector3 DeckOffsetFromCenter = new (-0.075f, 0, 0.075f);
    private float Player2DeckRotation = 180f;
    
    private PlayerController _playerOneController;
    private PlayerController _playerTwoController;
    private BoardController _board;
    
    protected override void Init()
    {
        _playerOneController = _gm.Player1Controller;
        _playerTwoController = _gm.Player2Controller;
        _board = _gm.BoardController;
    }

    public override void Execute()
    {
        var boardSurfaceCenter = _board.GetBoardSurfaceCenter();
        
        _playerOneController.PileUpCardsDeckView();
        _playerOneController.SetDeckPositionView(boardSurfaceCenter - DeckOffsetFromCenter);
        
        _playerTwoController.PileUpCardsDeckView();
        _playerTwoController.SetDeckPositionView(boardSurfaceCenter + DeckOffsetFromCenter);
        _playerTwoController.SetDeckRotationAngleView(Player2DeckRotation);
    }
}
