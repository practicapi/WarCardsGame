using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollectDrawnCardsToPlayersDeckCommand : BaseCommand<CollectDrawnCardsToPlayersDeckCommandData>
{
    private const float TotalSecondsToCollectAllCards = 1f;
    private const float SecondsShowingCardsRevealed = 0.5f;

    private readonly PlayerController _winnerPlayerController;
    private readonly BattleStateService _battleStateService;
    private readonly PlayerController _player1Controller;
    private readonly PlayerController _player2Controller;

    public CollectDrawnCardsToPlayersDeckCommand(CollectDrawnCardsToPlayersDeckCommandData data) : base(data)
    {
        _winnerPlayerController = _data.WinnerPlayerController;
        _battleStateService = _gm.BattleStateService;
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
    }

    public override async UniTask Execute()
    {
        if (_battleStateService.DidPerformAWarDuringCurrentBattle) // todo move ouside
        {
            await RevealAllCardsInDrawnPiles();
        }
        
        await CollectCards();

        _battleStateService.ResetBattleState(); // todo: move from here outside
    }

    private async Task CollectCards()
    {
        var numberOfCardsDrawnForEachPlayer = _player1Controller.GetDrawnCardsAmountData(); // it doesn't matter which player we choose,
                                                                                            // because they ALWAYS have the same number of drawn cards
        var numOfPlayers = 2;
        var secondsBetweenCardsCollected = TotalSecondsToCollectAllCards / (numberOfCardsDrawnForEachPlayer * numOfPlayers);
        var timeBetweenCardsCollected = TimeSpan.FromSeconds(secondsBetweenCardsCollected);

        for (int i = 0; i < numberOfCardsDrawnForEachPlayer; i++)
        {
            await CollectCardFromPlayersPile(_player1Controller, timeBetweenCardsCollected);
            await CollectCardFromPlayersPile(_player2Controller, timeBetweenCardsCollected);
        }
    }

    private async UniTask RevealAllCardsInDrawnPiles()
    {
        _player1Controller.RevealAllCardsInDrawnPile();
        _player2Controller.RevealAllCardsInDrawnPile();

        await UniTask.Delay(TimeSpan.FromSeconds(SecondsShowingCardsRevealed));
    }

    private async UniTask CollectCardFromPlayersPile(PlayerController playerController, TimeSpan timeToWait)
    {
        _winnerPlayerController.AddDeckCardsData(playerController.RemoveCardDataFromPile());
        var playerCardView = playerController.RemoveCardViewFromPile();
        _winnerPlayerController.AddDeckCardView(playerCardView);
        _winnerPlayerController.CollectCardToBottomOfDeckView(playerCardView).Forget();

        await UniTask.Delay(timeToWait);
    }
}