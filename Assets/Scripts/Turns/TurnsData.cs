
public class TurnsData
{
    public string CurrentTurnPlayerId { get; private set; }

    public TurnsData(string currentTurnPlayerId)
    {
        CurrentTurnPlayerId = currentTurnPlayerId;
    }

    public void SetCurrentTurnPlayerId(string playerId)
    {
        CurrentTurnPlayerId = playerId;
    }
}
