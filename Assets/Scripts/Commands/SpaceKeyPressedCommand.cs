using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpaceKeyPressedCommand : BaseCommand
{
    private BattleStateService _battleStateService;

    protected override void Init()
    {
        _battleStateService = _gm.BattleStateService;
    }

    public override async UniTask Execute()
    {
        if (_battleStateService.IsCurretlyDuringTurnSequece) return;

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
            
            Debug.Log("P1, DeckData: "+_gm.Player1Controller.GetDeckCardsAmountData+", DeckViewData: "+_gm.Player1Controller.GetDeckCardsAmountView);
            Debug.Log("P2, DeckData: "+_gm.Player2Controller.GetDeckCardsAmountData+", DeckViewData: "+_gm.Player2Controller.GetDeckCardsAmountView);
        }
    }
}