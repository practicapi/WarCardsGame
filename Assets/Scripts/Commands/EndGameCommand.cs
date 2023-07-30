using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EndGameCommand : BaseCommand<GameWinner>
{
    private readonly UIController _uiController;

    public EndGameCommand(GameWinner data) : base(data)
    {
       _uiController =  _gm.UIController;
    }

    public override async UniTask Execute()
    {
        _gm.BattleStateService.SetDoesHaveAGameWinner(true);
        var playerColor = _data == GameWinner.Player1 ? _gm.Player1Controller.Color : _gm.Player2Controller.Color;
        await _uiController.ShowWinnerText(_data, playerColor);
        _uiController.ShowRestartButton();
    }
}
