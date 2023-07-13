using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformTurnCommand : BaseCommand
{
    private TurnsService _turnService;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private BattleLogicService _battleLogicService;
    
    protected override void Init()
    {
        _turnService = _gm.TurnsService;
        _player1Controller = _gm.Player1Controller;
        _player2Controller = _gm.Player2Controller;
        _battleLogicService= _gm.BattleLogicService;
    }

    public override void Execute()
    {
        var player1Card = _player1Controller.DrawCardData();
        var player2Card = _player2Controller.DrawCardData();
        var cardsDrawnList = new List<CardData> {player1Card, player2Card};

        ExecuteTwoCardsBattle(player1Card, player2Card, cardsDrawnList);
        
        var nextStartingPlayer = _turnService.CurrentTurnPlayerId == _player1Controller.PlayerId ? _player1Controller : _player2Controller;
        _turnService.ChangeTurnToPlayer(nextStartingPlayer.PlayerId);
    }

    private BattleResult ExecuteTwoCardsBattle(CardData player1Card, CardData player2Card, List<CardData> cardsDrawnList)
    {
        var battleResult = _battleLogicService.CalculateBattleResult(player1Card.Value, player2Card.Value);
    
        switch (battleResult)
        {
            case BattleResult.Player1Win:
                ExecutePlayerWin(_player1Controller, cardsDrawnList);
                return BattleResult.Player1Win;
            case BattleResult.Player2Win:
                ExecutePlayerWin(_player2Controller, cardsDrawnList);
                return BattleResult.Player2Win;
            default:
                return ExecuteWar(cardsDrawnList);
        }
    }
    
    private void ExecutePlayerWin(PlayerController winnerPlayerController, List<CardData> cardsDrawnList)
    {
        winnerPlayerController.AddCardsData(cardsDrawnList.ToArray());
    }
    
    private BattleResult ExecuteWar(List<CardData> cardsDrawnList)
    {
        for (int i = 0; i < _battleLogicService.AmountOfFaceDownCardsWhenWar; i++)
        {
            cardsDrawnList.Add(_player1Controller.DrawCardData());
            cardsDrawnList.Add(_player2Controller.DrawCardData());
        }

        var player1Card = _player1Controller.DrawCardData();
        cardsDrawnList.Add(player1Card);
        var player2Card = _player2Controller.DrawCardData();
        cardsDrawnList.Add(player2Card);

        return ExecuteTwoCardsBattle(player1Card, player2Card, cardsDrawnList);
    }
}
