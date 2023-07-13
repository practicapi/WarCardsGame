using System.Collections;

public class BattleLogicService
{
    public int AmountOfFaceDownCardsWhenWar = 2;

    public BattleResult CalculateBattleResult(int player1Card, int player2Card)
    {
        ConvertCardsValueIfAces(ref player1Card, ref player2Card);
        
        if (player1Card > player2Card)
        {
            return BattleResult.Player1Win;
        }

        return player2Card > player1Card ? BattleResult.Player2Win : BattleResult.War;
    }

    private void ConvertCardsValueIfAces(ref int player1Card, ref int player2Card)
    {
        player1Card = ConvertCardValueIfAce(player1Card);
        player2Card = ConvertCardValueIfAce(player2Card);
    }
    
    private int ConvertCardValueIfAce(int cardNumber)
    {
        return cardNumber == 1 ? 14 : cardNumber;
    }
}

public enum BattleResult
{
    Player1Win,
    Player2Win,
    War
}
