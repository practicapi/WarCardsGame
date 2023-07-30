using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpaceKeyPressedCommand : BaseCommand
{
    private BattleStateService _battleStateService;
    private BattleLogicService _battleLogicService;

    protected override void Init()
    {
        _battleStateService = _gm.BattleStateService;
        _battleLogicService = _gm.BattleLogicService;
    }

    public override async UniTask Execute()
    {
        if (_battleStateService.IsCurretlyDuringTurnSequece || _battleStateService.DoesHaveAGameWinner) return;

        using (new SetCurrentlyDuringTurnDisposable())
        {
            switch (_battleStateService.CurrentBattleState)
            {
                case BattleState.Empty:
                    await new DrawOneCardForeachPlayerCommand().Execute();
                    break;
                case BattleState.Player1Win:
                    await new CollectDrawnCardsToPlayersDeckCommand(new CollectDrawnCardsToPlayersDeckCommandData(_gm.Player1Controller)).Execute();
                    break;
                case BattleState.Player2Win:
                    await new CollectDrawnCardsToPlayersDeckCommand(new CollectDrawnCardsToPlayersDeckCommandData(_gm.Player2Controller)).Execute();
                    break;
                case BattleState.War:
                    await new DrawFacedDownCardsForeachPlayerCommand().Execute();
                    await new DrawOneCardForeachPlayerCommand().Execute();
                    break;
            }
        }
    }
}