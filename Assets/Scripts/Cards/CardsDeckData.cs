using System;
using System.Collections.Generic;

public class CardsDeckData
{
    public Queue<CardData> Cards { get; private set; }

    public CardsDeckData(IEnumerable<CardData> cardsData)
    {
        Cards = new Queue<CardData>(cardsData);
    }
}
