using System;
using Cysharp.Threading.Tasks;

public class DrawFacedDownCardsForeachPlayerCommand : BaseCommand
{
    private const float SecondsBetweenDrawnCards = 0.3f;
    
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;

    protected override void Init()
    {
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
    }

    public override async UniTask Execute()
    {
        for (int i = 0; i < GameManager.Instance.BattleLogicService.AmountOfFaceDownCardsWhenWar; i++)
        {
            var card1Data = _player1Controller.MoveDecksFirstCardToPileData();
            var card2Data = _player2Controller.MoveDecksFirstCardToPileData();

            _player1Controller.DrawCardFromDeckToPileView(card1Data.Id, false).Forget();
            _player2Controller.DrawCardFromDeckToPileView(card2Data.Id, false).Forget();
            
            await UniTask.Delay(TimeSpan.FromSeconds(SecondsBetweenDrawnCards));
        }
    }
}