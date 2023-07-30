
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrepareObjectsViewCommand : BaseCommand
{
    private Vector3 DeckOffsetFromCenter = new (-0.075f, 0, 0.075f);
    private float Player2DeckRotation = 180f;
    
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private BoardController _board;
    
    protected override void Init()
    {
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
        _board = _gm.BoardController;
    }

    public override async UniTask Execute()
    {
        var boardSurfaceCenter = _board.GetBoardSurfaceCenter();
        
        _player1Controller.SetPositionView(boardSurfaceCenter);
        _player1Controller.PileUpCardsDeckView();
        _player1Controller.UpdateCardsLeftTextView();
        _player1Controller.SetupViews(/*DeckColor.Red, new Color(255, 52, 54, 255)*/);
            
        _player2Controller.SetPositionView(boardSurfaceCenter);
        _player2Controller.SetRotationAngleView(Player2DeckRotation);
        _player2Controller.PileUpCardsDeckView();
        _player2Controller.UpdateCardsLeftTextView();
        _player2Controller.SetupViews(/*DeckColor.Blue, new Color(55, 58, 119, 255)*/);
    }
}
