using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceKeyPressedCommand : BaseCommand
{
    protected override void Init()
    {
        
    }

    public override void Execute()
    {
        // if not in a middle of turn
        new PerformTurnCommand().Execute();
    }
}
