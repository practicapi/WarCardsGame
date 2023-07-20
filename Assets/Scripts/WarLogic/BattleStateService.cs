using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateService
{
    private BattleLogicService _battleLogicService;
    public BattleState CurrentBattleState { get; private set; }
    public bool DidPerformAWarDuringCurrentBattle { get; private set; }
    public BattleStateService()
    {
        _battleLogicService = new BattleLogicService();
    }

    public void ResetBattleState()
    {
        CurrentBattleState = BattleState.Empty;
        DidPerformAWarDuringCurrentBattle = false;
    }
    
    public void RecalculateCurrentBattleState(int player1Card, int player2Card)
    {
        CurrentBattleState = _battleLogicService.CalculateBattleResult(player1Card, player2Card);
    }
}
