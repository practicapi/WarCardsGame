using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DrawOneCardForeachPlayerCommand : BaseCommand
{
    private const float DelayInSecondsBetweenPlayersDraws = 0.3f;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private BattleStateService _battleStateService;

    protected override void Init()
    {
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
        _battleStateService =_gm.BattleStateService;
    }

    public override async UniTask Execute()
    {
        var player1Card = _player1Controller.MoveDecksFirstCardToPileData();
        var player2Card = _player2Controller.MoveDecksFirstCardToPileData();
        
        await DrawCardViewFromEachPlayer(player1Card, player2Card);
        
        _player1Controller.AddDeckCardsData();
        _battleStateService.RecalculateCurrentBattleState(player1Card.Value, player2Card.Value);
    }
    
    private async UniTask DrawCardViewFromEachPlayer(CardData player1Card, CardData player2Card)
    {
        _player1Controller.DrawCardFromDeckToPileView(player1Card.Id).Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(DelayInSecondsBetweenPlayersDraws));
        await _player2Controller.DrawCardFromDeckToPileView(player2Card.Id);
    }
}
