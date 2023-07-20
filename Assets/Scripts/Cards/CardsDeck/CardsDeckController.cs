using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class CardsDeckController
{
    private CardsDeckData _cardsDeckData;
    private CardsViewCreator _cardsViewCreator;
    private CardsDeckViewController _cardsDeckViewController;
    
    public CardsDeckController()
    {
        _cardsViewCreator = new CardsViewCreator();
        _cardsDeckViewController = new CardsDeckViewController();
    }

    public void CreateCardsView(Transform parent, DeckColor deckColor)
    {
        var cards = _cardsViewCreator.CreateCards(_cardsDeckData.Cards.ToArray(), out var deckParent);
        deckParent.transform.SetParent(parent);
        _cardsDeckViewController.SetDeck(cards, deckParent, deckColor);
    }

    public void PileUpCardsView()
    {
        var cardsIdsArray = _cardsDeckData.Cards.ToArray().Select(x => x.Id).ToArray();
        _cardsDeckViewController.PileUpCards(cardsIdsArray);
    }

    public void SetDeckViewPosition(Vector3 position)
    {
        _cardsDeckViewController.SetDeckPosition(position);
    }
    
    public Vector3 GetDeckViewPosition()
    {
        return _cardsDeckViewController.GetDeckViewPosition();
    }

    public void SetCardsData(IEnumerable<CardData> cardsData)
    {
        _cardsDeckData = new CardsDeckData(cardsData);
    }
    
    public void SetDeckRotationAngleView(float angle)
    {
        _cardsDeckViewController.SetDeckRotationAngle(angle);
    }

    public CardData DrawCardData()
    {
        return _cardsDeckData.DrawCard();
    }

    public void AddCardsData(CardData[] cardsData)
    {
        _cardsDeckData.AddCards(cardsData);
    }

    public CardView RemoveCardView(string cardId)
    {
        return _cardsDeckViewController.RemoveCard(cardId);
    }
    
    public async UniTask CollectCardToDeckBottomView(CardView cardView)
    {
        await _cardsDeckViewController.MoveCardToDeckBottom(cardView);
        _cardsDeckViewController.SetCardColorToDeckColor(cardView);
    }

    public void AddCardView(CardView cardView)
    {
        _cardsDeckViewController.AddCard(cardView);
    }

    public Vector3 GetDeckViewForward()
    {
        return _cardsDeckViewController.GetDeckViewForward();
    }
}
