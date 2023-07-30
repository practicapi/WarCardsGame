using System.Collections;
using UnityEngine;

public class BattleLogicService
{
    public int AmountOfFaceDownCardsWhenWar = 2;

    private bool DidPlayerLose(int playerCardsLeftAmount)
    {
        return playerCardsLeftAmount <= 0;
    }

    public bool CheckForGameWinner(int player1CardsLeftAmount, int player2CardsLeftAmount, out GameWinner gameWinner)
    {
        if (DidPlayerLose(player1CardsLeftAmount))
        {
            gameWinner = GameWinner.Player2;
            return true;
        }
        
        if (DidPlayerLose(player2CardsLeftAmount))
        {
            gameWinner = GameWinner.Player1;
            return true;
        }

        gameWinner = GameWinner.None;
        return false;
    }
    
    public BattleState CalculateBattleResult(int player1Card, int player2Card)
    {
        ConvertCardsValueIfAces(ref player1Card, ref player2Card);
        BattleState battleState = player1Card > player2Card ? BattleState.Player1Win : player1Card < player2Card ? BattleState.Player2Win : BattleState.War;
        
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

public enum GameWinner
{
    None,
    Player1,
    Player2
}


