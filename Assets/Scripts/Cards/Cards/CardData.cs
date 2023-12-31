using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    public string Id { get; private set; }
    public int Value { get; private set; }
    public CardSuit Suit { get; private set; }

    public CardData(string id, int value, CardSuit suit)
    {
        Id = id;
        Value = value;
        Suit = suit;
    }
}
