using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
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
}
