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
    private BattleLogicService _battleLogicService;

    protected override void Init()
    {
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
        _battleStateService =_gm.BattleStateService;
        _battleLogicService =_gm.BattleLogicService;
    }

    public override async UniTask Execute()
    {
        if (_battleLogicService.CheckForGameWinner(_player1Controller.DeckCardsAmount, _player2Controller.DeckCardsAmount, out var gameWinner))
        {
            await new EndGameCommand(gameWinner).Execute();
            await UniTask.FromCanceled();
        }
        
        var player1Card = _player1Controller.DrawCardFromDeckToPileData();
        var player2Card = _player2Controller.DrawCardFromDeckToPileData();
        
        await DrawCardViewFromEachPlayer(player1Card, player2Card);
        
        _battleStateService.RecalculateCurrentBattleState(player1Card.Value, player2Card.Value);
    }
    
    private async UniTask DrawCardViewFromEachPlayer(CardData player1Card, CardData player2Card)
    {
        _player1Controller.DrawCardFromDeckToPileView(player1Card.Id).Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(DelayInSecondsBetweenPlayersDraws));
        await _player2Controller.DrawCardFromDeckToPileView(player2Card.Id);
    }
}
