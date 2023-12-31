using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class CardsDeckController
{
    public int CardsAmountView => _cardsDeckViewController.CardsAmount;
    private CardsDeckData _cardsDeckData;
    private CardsViewCreator _cardsViewCreator;
    private CardsDeckViewController _cardsDeckViewController;
    public int CardsAmount => _cardsDeckData.Cards.Count;

    public CardsDeckController()
    {
        _cardsViewCreator = new CardsViewCreator();
        _cardsDeckViewController = new CardsDeckViewController();
    }

    public void CreateCardsView(Transform parent)
    {
        var cards = _cardsViewCreator.CreateCards(_cardsDeckData.Cards.ToArray());
        _cardsDeckViewController.SetDeck(cards, parent);
    }

    public void SetupView()
    {
        _cardsDeckViewController.SetBackFaceTextureToAllCards(_cardsDeckData.DeckBackFaceTexture);
    }

    public void PileUpCardsView()
    {
        var cardsIdsArray = _cardsDeckData.Cards.ToArray().Select(x => x.Id).ToArray();
        _cardsDeckViewController.PileUpCards(cardsIdsArray);
    }

    public void SetData(DeckData deckData, IEnumerable<CardData> cardsData)
    {
        _cardsDeckData = new CardsDeckData(deckData, cardsData);
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
        _cardsDeckViewController.AddCard(cardView);
        await _cardsDeckViewController.MoveCardToDeckBottom(cardView);
        _cardsDeckViewController.SetBackFaceTextureToCard(cardView, _cardsDeckData.DeckBackFaceTexture);
        _cardsDeckViewController.ResetCardRotation(cardView);
    }
}
