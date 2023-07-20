using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollectDrawnCardsToPlayersDeckCommand : BaseCommand<CollectDrawnCardsToPlayersDeckCommandData>
{
    private const float SecondsBetweenCardsCollected = 0.25f;
    
    private readonly PlayerController _winnerPlayerController;
    private readonly BattleStateService _battleStateService;

    public CollectDrawnCardsToPlayersDeckCommand(CollectDrawnCardsToPlayersDeckCommandData data) : base(data)
    {
        _winnerPlayerController = _data.WinnerPlayerController;
        _battleStateService = _gm.BattleStateService;
    }

    public override async UniTask Execute()
    {
        var player1Controller = _gm.Player1Controller;
        var player2Controller = _gm.Player2Controller;
        
        // it doesn't matter which player we choose, because they have the same number of drawn cards
        var numberOfCardsDrawnForEachPlayer = player1Controller.GetDrawnCardsData();

        for (int i = 0; i < numberOfCardsDrawnForEachPlayer.Count; i++)
        {
            CollectCardFromPlayersPile(player1Controller).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(SecondsBetweenCardsCollected));
            CollectCardFromPlayersPile(player2Controller).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(SecondsBetweenCardsCollected));
        }

        _battleStateService.ResetBattleState(); // todo: move from here outside

        //
        // foreach (var drawnCard in totalDrawnCardsData)
        // {
        //     
        // }
        //
        //
        // winnerPlayerController.AddDeckCardsData(totalDrawnCardsData);
        //
        // var player1DrawnCardsView = _player1Controller.GetDrawnCardsView();
        // var player2DrawnCardsView = _player2Controller.GetDrawnCardsView();
        // var totalDrawnCardsView = player1DrawnCardsView.Concat(player2DrawnCardsView).ToArray();
        //
        // await winnerPlayerController.AddCardsToDeckBottomView(totalDrawnCardsData);
        //
        // _player1Controller.ClearCardsDrawnData();
        // _player2Controller.ClearCardsDrawnData();
    }

    private async UniTask CollectCardFromPlayersPile(PlayerController playerController)
    {
        var playerCardView = playerController.RemoveCardViewFromPile();
        _winnerPlayerController.AddDeckCardView(playerCardView);
        await _winnerPlayerController.CollectCardToBottomOfDeckView(playerCardView);
        
        _winnerPlayerController.AddDeckCardsData(playerController.RemoveCardDataFromPile());
    }
}