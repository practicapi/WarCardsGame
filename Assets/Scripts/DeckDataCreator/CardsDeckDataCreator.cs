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
            for (int value = 0; value < AmountOfCardsValues; value++)
            {
                var cardUniqueId = value + 1 + suit * AmountOfCardsValues;
                var cardSuit = (CardSuit) suit;
                var cardData = new CardData(cardUniqueId, value, cardSuit);

                cards[suit * AmountOfCardsValues + value] = cardData;
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
