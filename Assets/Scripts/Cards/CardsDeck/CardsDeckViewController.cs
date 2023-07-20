using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class CardsDeckViewController
{
    private const float PileVerticalSpaceBetweenCards = 0.0005f;
    private Dictionary<string,CardView> _cards;
    private Transform _deckParent;
    private Vector3 _deckBottomPoint;
    private DeckColor _deckColor;

    public void SetDeck(List<CardView> cards, GameObject deckParent, DeckColor deckColor)
    {
        _deckColor = deckColor;
        _deckParent = deckParent.transform;
        SetCards(cards);
    }

    private void SetCards(List<CardView> cards)
    {
        _cards = new Dictionary<string, CardView>();
        cards.ForEach(AddCard);
        cards.ForEach(SetCardColorToDeckColor);
    }

    public void SetCardColorToDeckColor(CardView cardView)
    {
        cardView.SetColor(_deckColor);
    }

    public void PileUpCards(string[] cardsIds)
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
        _deckParent.position = position + (halfOfDeckHeight + PileVerticalSpaceBetweenCards) * Vector3.up;
        _deckBottomPoint = position;
    }

    public void SetDeckRotationAngle(float angle)
    {
        Vector3 rotationVector = new Vector3(0, angle, 0);
        _deckParent.localRotation = Quaternion.Euler(rotationVector);
    }

    public CardView RemoveCard(string cardId)
    {
        var cardView = _cards[cardId];
        _cards.Remove(cardId);
        return cardView;
    }
    
    public async UniTask MoveCardToDeckBottom(CardView cardView)
    {
        await cardView.MoveToPointFacedDown(_deckBottomPoint);
        cardView.transform.SetParent(_deckParent);
        _deckParent.Translate(Vector3.up*PileVerticalSpaceBetweenCards);
    }

    public void AddCard(CardView cardView)
    {
        _cards.Add(cardView.ID, cardView);
    }

    public Vector3 GetDeckViewPosition()
    {
        return _deckParent.position;
    }

    public Vector3 GetDeckViewForward()
    {
        return _deckParent.forward;
    }
}