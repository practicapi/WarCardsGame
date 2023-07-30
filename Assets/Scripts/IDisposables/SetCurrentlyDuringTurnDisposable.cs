using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetCurrentlyDuringTurnDisposable : IDisposable
{

    public SetCurrentlyDuringTurnDisposable()
    {
        GameManager.Instance.BattleStateService.SetIsDuringTurnSequence(true);
    }
    
    public void Dispose()
    {
        GameManager.Instance.BattleStateService.SetIsDuringTurnSequence(false);
    }
}
