using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardsDeckController
{
    private CardsDeckData _cardsDeckData;
    private CardsViewCreator _cardsViewCreator;
    private CardsDeckView _cardsDeckView;
    
    public CardsDeckController()
    {
        _cardsViewCreator = new CardsViewCreator();
        _cardsDeckView = new CardsDeckView();
    }

    public void CreateCardsView()
    {
        var cards = _cardsViewCreator.CreateCards(_cardsDeckData.Cards.ToArray(), out var deckParent);
        _cardsDeckView.SetDeck(cards, deckParent);
    }

    public void PileUpCardsView()
    {
        var cardsIdsArray = _cardsDeckData.Cards.ToArray().Select(x => x.Id).ToArray();
        _cardsDeckView.PileUpCards(cardsIdsArray);
    }

    public void SetDeckViewPosition(Vector3 position)
    {
        _cardsDeckView.SetDeckPosition(position);
    }

    public void SetCardsData(DeckColor deckColor, IEnumerable<CardData> cardsData)
    {
        _cardsViewCreator.SetDeckColor(deckColor);
        _cardsDeckData = new CardsDeckData(cardsData);
    }
    
    public void SetDeckRotationAngleView(float angle)
    {
        _cardsDeckView.SetDeckRotationAngle(angle);
    }

    public CardData DrawCardData()
    {
        return _cardsDeckData.DrawCard();
    }

    public void AddCardsData(CardData[] cardsData)
    {
        _cardsDeckData.AddCards(cardsData);
    }
}
