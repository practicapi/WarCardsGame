using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerWinBattleCommand : BaseCommand<PlayerController>
{
    private readonly PlayerController _playerWonController;

    public PlayerWinBattleCommand(PlayerController playerWonController) : base(playerWonController)
    {
        _playerWonController = playerWonController;
    }

    public override async UniTask Execute()
    {
        
    }
}
