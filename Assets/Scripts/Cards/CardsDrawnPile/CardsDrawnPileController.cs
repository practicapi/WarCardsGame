using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDrawnPileController
{
    private CardsDrawnPileViewController _cardsDrawnPileViewController;
    private CardsDrawnPileData _cardsDrawnPileData;

    public CardsDrawnPileController()
    {
        _cardsDrawnPileViewController = new CardsDrawnPileViewController();
        _cardsDrawnPileData = new CardsDrawnPileData();
    }
    
    public void CreatePileView(Transform parent)
    {
        _cardsDrawnPileViewController.CreatePile(parent);
    }

    public void AddCardDataToPile(CardData cardData)
    {
        _cardsDrawnPileData.AddCard(cardData);
    }
    
    public CardData RemoveCardDataFromPile()
    {
        return _cardsDrawnPileData.RemoveCard();
    }
    
    public void AddCardViewToPile(CardView cardView)
    {
        _cardsDrawnPileViewController.AddCardToPile(cardView);
    }

    public async UniTask AnimateDrawCardViewToPile(CardView cardView, bool shouldFaceUp = true)
    {
        await _cardsDrawnPileViewController.AnimateDrawCardToPile(cardView, shouldFaceUp);
    }

    public CardView RemoveCardViewFromPile()
    {
        return _cardsDrawnPileViewController.RemoveCardFromPile();
    }

    public Stack<CardData> GetDrawnCardsData()
    {
        return _cardsDrawnPileData.CardsData;
    }

    public void ClearCardsDrawnData()
    {
        _cardsDrawnPileData.ClearCards();
    }

    public void SetPileRotationAngleView(float angle)
    {
        _cardsDrawnPileViewController.SetPileRotationAngleView(angle);
    }

    public void RevealAllCardsInDrawnPile()
    {
        _cardsDrawnPileViewController.RevealAllCardsInDrawnPile();
    }
}

public class CardsDrawnPileData
{
    public Stack<CardData> CardsData { get; private set; }

    public CardsDrawnPileData()
    {
        CardsData = new Stack<CardData>();
    }

    public void AddCard(CardData cardData)
    {
        CardsData.Push(cardData);
    }

    public CardData RemoveCard()
    {
        return CardsData.Pop();
    }

    public void ClearCards()
    {
        CardsData.Clear();
    }
}