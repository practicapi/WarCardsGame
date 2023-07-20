using System;

public class CardsDeckDataCreator
{
    private const int AmountOfSuits = 4;
    private const int AmountOfCardsValues = 13;

    public CardData[] CreateFullShuffledDeck()
    {
        var cards = CreateFullDeck();
        ShuffleDeck(cards);

        return cards;
    }
    
    private CardData[] CreateFullDeck()
    {
        var cards = new CardData[AmountOfSuits * AmountOfCardsValues];
        
        for (int suit = 0; suit < AmountOfSuits; suit++)
        {
            for (int value = 1; value <= AmountOfCardsValues; value++)
            {
                var cardIndex = suit * AmountOfCardsValues + value - 1;
                var cardSuit = (CardSuit) suit;
                var cardData = new CardData(cardIndex.ToString(), value, cardSuit);

                cards[cardIndex] = cardData;
            }
        }

        return cards;
    }

    private void ShuffleDeck(CardData[] cards)
    {
        Random rnd = new Random();
        rnd.Shuffle(cards);
    }
}
