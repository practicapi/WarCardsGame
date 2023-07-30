using System;
using Cysharp.Threading.Tasks;

public class DrawFacedDownCardsForeachPlayerCommand : BaseCommand
{
    private const float SecondsBetweenDrawnCards = 0.3f;
    
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private BattleLogicService _battleLogicService;

    protected override void Init()
    {
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
        _battleLogicService = _gm.BattleLogicService;
    }

    public override async UniTask Execute()
    {
        var amountOfFaceDownCardsWhenWar = _battleLogicService.AmountOfFaceDownCardsWhenWar;
        var lastCardIndex = amountOfFaceDownCardsWhenWar - 1;
        
        for (int i = 0; i < amountOfFaceDownCardsWhenWar; i++)
        {
            if (_battleLogicService.CheckForGameWinner(
                    _player1Controller.DeckCardsAmount, _player2Controller.DeckCardsAmount, out var gameWinner))
            {
                await new EndGameCommand(gameWinner).Execute();
                await  UniTask.FromCanceled();
            }
            
            var isLastCard = i == lastCardIndex;

            if (isLastCard)
            {
                await DrawCardFromEachPlayer();
            }
            else
            {
                DrawCardFromEachPlayer().Forget();
                await UniTask.Delay(TimeSpan.FromSeconds(SecondsBetweenDrawnCards));
            }
        }
    }

    private UniTask DrawCardFromEachPlayer()
    {
        DrawCardFromPlayer(_player1Controller).Forget();
        return DrawCardFromPlayer(_player2Controller);
    }

    private async UniTask DrawCardFromPlayer(PlayerController player)
    {
        var cardData = player.DrawCardFromDeckToPileData();
        await player.DrawCardFromDeckToPileView(cardData.Id, false);
    }
}