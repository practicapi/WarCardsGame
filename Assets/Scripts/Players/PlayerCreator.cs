using UnityEngine;

public class PlayerCreator
{
    private const string PlayerPath = "Prefabs/Player";

    public PlayerView CreatePlayer(string nameSuffix)
    {
        var playerGO = GameObject.Instantiate(Resources.Load<GameObject>(PlayerPath));
        playerGO.name = playerGO.name + "_" + nameSuffix;
        return playerGO.GetComponent<PlayerView>();
    }
}
