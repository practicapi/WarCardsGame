using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class PerformTurnCommand : BaseCommand
{
    private const float DelayInSecondsBetweenPlayersDraws = 0.3f;
    
    private TurnsService _turnService;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    
    protected override void Init()
    {
        _turnService = _gm.TurnsService;
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
    }

    public override async UniTask Execute()
    {
        var player1Card = _player1Controller.DrawCardFromDeckToPileData();
        var player2Card = _player2Controller.DrawCardFromDeckToPileData();
        
        await DrawCardFromEachPlayer(player1Card, player2Card);

        var cardsDrawnList = new List<CardData> {player1Card, player2Card};

        ExecuteTwoCardsBattle(player1Card, player2Card, cardsDrawnList);
        
        var nextStartingPlayer = _turnService.CurrentTurnPlayerId == _player1Controller.PlayerId ? _player1Controller : _player2Controller;
        _turnService.ChangeTurnToPlayer(nextStartingPlayer.PlayerId);
    }

    private async Task DrawCardFromEachPlayer(CardData player1Card, CardData player2Card)
    {
        _player1Controller.DrawCardFromDeckToPileView(player1Card.Id).Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(DelayInSecondsBetweenPlayersDraws));
        await _player2Controller.DrawCardFromDeckToPileView(player2Card.Id);
    }

    private BattleState ExecuteTwoCardsBattle(CardData player1Card, CardData player2Card, List<CardData> cardsDrawnList)
    {
        // var battleResult = _battleLogicService.CalculateBattleResult(player1Card.Value, player2Card.Value);
        //
        // switch (battleResult)
        // {
        //     case BattleState.Player1Win:
        //         ExecutePlayerWin(_player1Controller, cardsDrawnList);
        //         return BattleState.Player1Win;
        //     case BattleState.Player2Win:
        //         ExecutePlayerWin(_player2Controller, cardsDrawnList);
        //         return BattleState.Player2Win;
        //     default:
        //         return ExecuteWar(cardsDrawnList);
        // }
        
        return BattleState.Empty;
    }
    
    private async UniTask ExecutePlayerWin(PlayerController winnerPlayerController, List<CardData> cardsDrawnList)
    {
        winnerPlayerController.AddDeckCardsData(cardsDrawnList.ToArray());
        var cardsIds = cardsDrawnList.Select(x => x.Id).ToList();
        await winnerPlayerController.CollectCardsView(cardsIds);
    }
    
    private BattleState ExecuteWar(List<CardData> cardsDrawnList)
    {
        // for (int i = 0; i < _battleLogicService.AmountOfFaceDownCardsWhenWar; i++)
        // {
        //     cardsDrawnList.Add(_player1Controller.MoveDecksFirstCardToPileData());
        //     cardsDrawnList.Add(_player2Controller.MoveDecksFirstCardToPileData());
        // }
        //
        // var player1Card = _player1Controller.MoveDecksFirstCardToPileData();
        // cardsDrawnList.Add(player1Card);
        // var player2Card = _player2Controller.MoveDecksFirstCardToPileData();
        // cardsDrawnList.Add(player2Card);
        //
        // return ExecuteTwoCardsBattle(player1Card, player2Card, cardsDrawnList);
        return BattleState.Empty;
    }
}
