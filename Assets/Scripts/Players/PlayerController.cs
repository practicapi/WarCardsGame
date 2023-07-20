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
    private PlayerData _playerData;
    private Transform _playerView;
    
    public PlayerController()
    {
        _deckController = new CardsDeckController();
        _cardsDrawnPileController = new CardsDrawnPileController();
    }

    public void SetData(string playerName, IEnumerable<CardData> cardsData)
    {
        _playerData = new PlayerData(playerName);
        _deckController.SetCardsData(cardsData);
    }
    
    public void CreateCardsView(DeckColor deckColor)
    {
        _playerView = new GameObject("Player_" + _playerData.Id.Substring(0,3)).transform;
        _deckController.CreateCardsView(_playerView, deckColor);
        _cardsDrawnPileController.CreatePileView(_playerView);
    }  
    
    public void PileUpCardsDeckView()
    {
        _deckController.PileUpCardsView();
    }

    public void SetDeckPositionView(Vector3 position)
    {
        _deckController.SetDeckViewPosition(position);
    }

    public void UpdateDrawnPilePositionRelativeToDeckView()
    {
        var deckPosition = _deckController.GetDeckViewPosition();
        var deckForwardVector = _deckController.GetDeckViewForward();
        _cardsDrawnPileController.SetPileViewPosition(deckPosition + DistanceBetweenDeckAndPile *deckForwardVector);
    }

    public void SetDeckRotationAngleView(float angle)
    {
        _deckController.SetDeckRotationAngleView(angle);
    }

    public CardData MoveDecksFirstCardToPileData()
    {
        var card = _deckController.DrawCardData();
        Debug.Log("Draw p"+PlayerId.Substring(0,3)+": "+card.Value);
        _cardsDrawnPileController.AddCardDataToPile(card);

        return card;
    }

    public void AddDeckCardsData(params CardData[] cardsData)
    {
        _deckController.AddCardsData(cardsData);
    }

    public async UniTask DrawCardFromDeckToPileView(string cardId)
    {
        var cardView = _deckController.RemoveCardView(cardId);
        await _cardsDrawnPileController.DrawCardViewToPile(cardView);
        _cardsDrawnPileController.AddCardViewToPile(cardView);
    }

    public async UniTask CollectCardsView(List<string> cardsIds)
    {
        //_deckController.CollectCardsView
    }

    public Stack<CardView> GetDrawnCardsView()
    {
        return _cardsDrawnPileController.GetDrawnCardsView();
    }
    
    public Stack<CardData> GetDrawnCardsData()
    {
        return _cardsDrawnPileController.GetDrawnCardsData();
    }

    public CardData RemoveCardDataFromPile()
    {
        return _cardsDrawnPileController.RemoveCardDataFromPile();
    }

    public void ClearCardsDrawnData()
    {
        _cardsDrawnPileController.ClearCardsDrawnData();
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
}