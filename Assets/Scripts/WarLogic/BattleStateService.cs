using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateService
{
    public BattleState CurrentBattleState { get; private set; } = BattleState.Empty;
    public bool DidPerformAWarDuringCurrentBattle { get; private set; }
    
    public BattleStateService()
    {
    }

    public void ResetBattleState()
    {
        CurrentBattleState = BattleState.Empty;
        DidPerformAWarDuringCurrentBattle = false;
        Debug.Log(CurrentBattleState);
    }
    
    public void RecalculateCurrentBattleState(int player1Card, int player2Card)
    {
        var battleLogicService = GameManager.Instance.BattleLogicService;
        CurrentBattleState = battleLogicService.CalculateBattleResult(player1Card, player2Card);
        Debug.Log(CurrentBattleState);
    }
}
