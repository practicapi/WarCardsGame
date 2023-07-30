using System;
using System.Collections.Generic;
using UnityEngine;

public class CardsDeckData
{
    private readonly DeckData _deckData;
    public Queue<CardData> Cards { get; private set; }
    public Texture2D DeckBackFaceTexture => _deckData.CardBackFaceTexture;
    
    public CardsDeckData(DeckData deckData, IEnumerable<CardData> cardsData)
    {
        _deckData = deckData;
        Cards = new Queue<CardData>(cardsData);
    }

    public CardData DrawCard()
    {
        return Cards.Dequeue();
    }

    public void AddCards(CardData[] cardsData)
    {
        cardsData.ForEach(cardData => Cards.Enqueue(cardData));
    }
}
