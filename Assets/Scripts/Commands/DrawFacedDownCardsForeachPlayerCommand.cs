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
        var amountOfFaceDownCardsWhenWar = GameManager.Instance.BattleLogicService.AmountOfFaceDownCardsWhenWar;
        var lastCardIndex = amountOfFaceDownCardsWhenWar - 1;
        for (int i = 0; i < amountOfFaceDownCardsWhenWar; i++)
        {
            DrawCardFromPlayer(_player1Controller).Forget();
            var drawCardFromPlayer2Task = DrawCardFromPlayer(_player2Controller);
            var isLastCard = i == lastCardIndex;

            if (isLastCard)
            {
                await drawCardFromPlayer2Task;
            }
            else
            {
                drawCardFromPlayer2Task.Forget();
                await UniTask.Delay(TimeSpan.FromSeconds(SecondsBetweenDrawnCards));
            }
        }
    }

    private async UniTask DrawCardFromPlayer(PlayerController player)
    {
        var cardData = player.DrawCardFromDeckToPileData();
        await player.DrawCardFromDeckToPileView(cardData.Id, false);
    }
}