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
        if (_battleStateService.DidPerformAWarDuringCurrentBattle) // todo move outside
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
        var totalNumbersOfCardsDrawnFromAllPlayers = numberOfCardsDrawnForEachPlayer * numOfPlayers;
        var secondsBetweenCardsCollected = TotalSecondsToCollectAllCards / totalNumbersOfCardsDrawnFromAllPlayers;
        var timeBetweenCardsCollected = TimeSpan.FromSeconds(secondsBetweenCardsCollected);
        var lastCardIndex = totalNumbersOfCardsDrawnFromAllPlayers - 1;
        
        for (int i = 0; i < totalNumbersOfCardsDrawnFromAllPlayers; i++)
        {
            var currentPlayerToCollectCardFrom = i % numOfPlayers == 0 ? _player1Controller : _player2Controller;
            var collectCardFromPlayersPileTask = CollectCardFromPlayersPile(currentPlayerToCollectCardFrom);

            if (i == lastCardIndex)
            {
                await collectCardFromPlayersPileTask;
            }
            else
            {
                collectCardFromPlayersPileTask.Forget();
                await UniTask.Delay(timeBetweenCardsCollected);
            }
        }
    }

    private async UniTask RevealAllCardsInDrawnPiles()
    {
        _player1Controller.RevealAllCardsInDrawnPile();
        _player2Controller.RevealAllCardsInDrawnPile();

        await UniTask.Delay(TimeSpan.FromSeconds(SecondsShowingCardsRevealed));
    }

    private async UniTask CollectCardFromPlayersPile(PlayerController playerController)
    {
        var playerCardView = playerController.RemoveCardViewFromPile();
        await _winnerPlayerController.CollectCardToBottomOfDeckView(playerCardView);
        _winnerPlayerController.AddDeckCardsData(playerController.RemoveCardDataFromPile());
        _winnerPlayerController.UpdateCardsLeftTextView();
    }
}