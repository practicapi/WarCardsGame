using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController
{
    private const float DistanceBetweenDeckAndPile = 0.1f;
    
    public  string PlayerId => _playerData.Id;
    private CardsDeckController _deckController;
    private CardsDrawnPileController _cardsDrawnPileController;
    private CardsLeftTextController _cardsLeftTextController;
    private PlayerCreator _playerCreator;
    private PlayerData _playerData;
    private PlayerView _playerView;
    
    public PlayerController()
    {
        _deckController = new CardsDeckController();
        _cardsDrawnPileController = new CardsDrawnPileController();
        _cardsLeftTextController = new CardsLeftTextController();
        _playerCreator = new PlayerCreator();
    }

    public void SetData(PlayerData playerData, IEnumerable<CardData> cardsData)
    {
        _playerData = playerData;
        _deckController.SetData(playerData.DeckData, cardsData);
        _cardsLeftTextController.SetData(playerData.CardLeftTextData);
    }
    
    public void CreateViews()
    {
        var nameSuffix = _playerData.Id;
        _playerView = _playerCreator.CreatePlayer(nameSuffix);
        _deckController.CreateCardsView(_playerView.DeckParentTransform);
        _cardsDrawnPileController.CreatePileView(_playerView.PileParentTransform);
        _cardsLeftTextController.CreateTextView(_playerView.CardsLeftTextParentTransform);
    }  
    
    public void SetupViews()
    {
        _deckController.SetupView();
        _cardsLeftTextController.SetupView();
    }  
    
    public void PileUpCardsDeckView()
    {
        _deckController.PileUpCardsView();
    }

    public CardData MoveDecksFirstCardToPileData()
    {
        var card = _deckController.DrawCardData();
        _cardsDrawnPileController.AddCardDataToPile(card);

        return card;
    }

    public void AddDeckCardsData(params CardData[] cardsData)
    {
        _deckController.AddCardsData(cardsData);
    }

    public async UniTask DrawCardFromDeckToPileView(string cardId, bool isFacedUp = true)
    {
        var cardView = _deckController.RemoveCardView(cardId);
        _cardsDrawnPileController.AddCardViewToPile(cardView);
        UpdateCardsLeftTextView();
        await _cardsDrawnPileController.DrawCardViewToPile(cardView, isFacedUp);
    }

    public async UniTask CollectCardsView(List<string> cardsIds)
    {
        //_deckController.CollectCardsView
    }

    public int GetDrawnCardsAmountData()
    {
        return _cardsDrawnPileController.GetDrawnCardsData().Count;
    }

    public CardData RemoveCardDataFromPile()
    {
        return _cardsDrawnPileController.RemoveCardDataFromPile();
    }

    public void AddDeckCardView(CardView cardView)
    {
        _deckController.AddCardView(cardView);
    }

    public CardView RemoveCardViewFromPile()
    {
        return _cardsDrawnPileController.RemoveCardViewFromPile();
    }

    public async UniTask CollectCardToBottomOfDeckView(CardView cardView)
    {
        await _deckController.CollectCardToDeckBottomView(cardView);
    }

    public void RevealAllCardsInDrawnPile()
    {
        _cardsDrawnPileController.RevealAllCardsInDrawnPile();
    }

    public void SetPositionView(Vector3 boardSurfaceCenter)
    {
        _playerView.transform.position = boardSurfaceCenter;
        _deckController.SetStartingPosition();
    }

    public void UpdateCardsLeftTextView()
    {
        _cardsLeftTextController.SetNumberView(_deckController.CardsAmount);
    }

    public void SetRotationAngleView(float angle)
    {
        _playerView.transform.localRotation = angle.ToQuaternionAroundYAxis();
    }
}