using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsService
{
    public string CurrentTurnPlayerId => _turnsData.CurrentTurnPlayerId;
    
    private readonly TurnsData _turnsData;
    
    public TurnsService(string currentTurnPlayerId)
    { 
        _turnsData = new TurnsData(currentTurnPlayerId);
    }

    public void ChangeTurnToPlayer(string playerId)
    {
        _turnsData.SetCurrentTurnPlayerId(playerId);
    }
}
