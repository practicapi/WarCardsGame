using System.Collections;
using UnityEngine;

public class BattleLogicService
{
    public int AmountOfFaceDownCardsWhenWar = 2;

    public BattleState CalculateBattleResult(int player1Card, int player2Card)
    {
        ConvertCardsValueIfAces(ref player1Card, ref player2Card);
        BattleState battleState;
        
        if (player1Card > player2Card)
        {
            battleState = BattleState.Player1Win;
        }
        else
        {
            battleState= player2Card > player1Card ? BattleState.Player2Win : BattleState.War;
        }

        if (true)
        {
            battleState = Mathf.Abs(player2Card-player1Card)<=4 ? BattleState.War: battleState;
        }

        return battleState;
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

public enum BattleState
{
    Empty,
    Player1Win,
    Player2Win,
    War
}
