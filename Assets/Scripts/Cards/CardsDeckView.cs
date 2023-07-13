using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsDeckView
{
    private const float PileVerticalSpaceBetweenCards = 0.0005f;
    private Dictionary<int,CardView> _cards;
    private GameObject _deckParent;

    public CardsDeckView()
    {
        
    }

    public void SetDeck(List<CardView> cards, GameObject deckParent)
    {
        _deckParent = deckParent;
        SetCards(cards);
    }

    private void SetCards(List<CardView> cards)
    {
        _cards = new Dictionary<int, CardView>();
        cards.ForEach(x => _cards.Add(x.ID, x));
    }

    public void PileUpCards(int[] cardsIds)
    {
        for (int i = 0; i < cardsIds.Length; i++)
        {
            var cardId = cardsIds[i];
            var cardView = _cards[cardId];
            var cardTransform = cardView.transform;
            var cardPos = cardTransform.position;
            var newPositionInPile = new Vector3(cardPos.x, cardPos.y + PileVerticalSpaceBetweenCards * i, cardPos.z);
            
            cardTransform.position = newPositionInPile;
            var faceDownRotation = Quaternion.Euler(0,0,180);
            cardTransform.localRotation = faceDownRotation;
        }
    }

    public void SetDeckPosition(Vector3 position)
    {
        var halfOfDeckHeight = PileVerticalSpaceBetweenCards * _cards.Count;
        _deckParent.transform.position = position + (halfOfDeckHeight + PileVerticalSpaceBetweenCards) * Vector3.up;
    }

    public void OpenCard(int cardId, Vector3 destinationPoint)
    {
        var cardView = _cards[cardId];
        cardView.RotateToPoint(destinationPoint);
    }
}
