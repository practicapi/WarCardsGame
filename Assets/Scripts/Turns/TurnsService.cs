public class TurnsService
{
    public string CurrentTurnPlayerId => _turnsData.CurrentTurnPlayerId;

    private TurnsData _turnsData;

    public void SetData(string startingPlayerId)
    {
        _turnsData = new TurnsData(startingPlayerId);
    }
    
    public void ChangeTurnToPlayer(string playerId)
    {
        _turnsData.SetCurrentTurnPlayerId(playerId);
    }
}
