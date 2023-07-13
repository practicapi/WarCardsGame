using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public  string PlayerId => _playerData.Id;
    private CardsDeckController _deckController;
    private PlayerData _playerData;
    
    public PlayerController()
    {
        _deckController = new CardsDeckController();
    }

    public void SetData(string playerName, DeckColor deckColor, IEnumerable<CardData> cardsData)
    {
        _playerData = new PlayerData(playerName);
        _deckController.SetCardsData(deckColor, cardsData);
    }
    
    public void CreateCardsView()
    {
        _deckController.CreateCardsView();
    }  
    
    public void PileUpCardsDeckView()
    {
        _deckController.PileUpCardsView();
    }

    public void SetDeckPositionView(Vector3 position)
    {
        _deckController.SetDeckViewPosition(position);
    }

    public void SetDeckRotationAngleView(float angle)
    {
        _deckController.SetDeckRotationAngleView(angle);
    }

    public CardData DrawCardData()
    {
        var card = _deckController.DrawCardData();
        Debug.Log("Draw p"+PlayerId.Substring(0,3)+": "+card.Value);
        return card;
    }

    public void AddCardsData(params CardData[] cardsData)
    {
        _deckController.AddCardsData(cardsData);
    }
}
