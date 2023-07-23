using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDeckViewController
{
    private static readonly Quaternion FaceDownRotation = Quaternion.Euler(0,0,180);
    private const float PileVerticalSpaceBetweenCards = 0.0005f;
    
    private Dictionary<string,CardView> _cards;
    private Transform _deckParent;
    private Vector3 _deckBottomPoint;
    private DeckColor _deckColor;

    public void SetDeck(List<CardView> cards, Transform decksParent, DeckColor deckColor)
    {
        _deckColor = deckColor;
        
        _deckParent = new GameObject("Deck").transform;
        _deckParent.SetParent(decksParent);
        _deckParent.position = Vector3.zero;
        
        SetCards(cards);
    }

    private void SetCards(List<CardView> cards)
    {
        _cards = new Dictionary<string, CardView>();
        
        foreach(var cardView in cards)
        {
            AddCard(cardView);
            SetCardColorToDeckColor(cardView);
        }
    }

    public void SetCardColorToDeckColor(CardView cardView)
    {
        cardView.SetColor(_deckColor);
    }

    public void PileUpCards(string[] topToBottomCardIds)
    {
        var cardsAmount = topToBottomCardIds.Length;
        
        for (int i = 0; i < cardsAmount; i++)
        {
            string cardId = topToBottomCardIds[i];
            var cardView = _cards[cardId];
            cardView.transform.position = _deckBottomPoint + (cardsAmount-i+1) * PileVerticalSpaceBetweenCards * Vector3.up;
            ResetCardRotation(cardView);
        }
    }

    private void MoveDeckOneUp()
    {
        _deckParent.Translate(Vector3.up*PileVerticalSpaceBetweenCards);
    }

    public void SetDeckStartingPosition(Vector3 position)
    {
        _deckParent.position = position;
        _deckBottomPoint = position;
    }

    public void SetDeckRotationAngle(float angle)
    {
        _deckParent.localRotation = angle.ToQuaternionAroundYAxis();
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
        MoveDeckOneUp();
    }

    public void AddCard(CardView cardView)
    {
        _cards.Add(cardView.ID, cardView);
        cardView.transform.SetParent(_deckParent);
    }

    public Vector3 GetDeckViewPosition()
    {
        return _deckParent.position;
    }

    public Vector3 GetDeckViewForward()
    {
        return _deckParent.forward;
    }

    public void ResetCardRotation(CardView cardView)
    {
        cardView.transform.localRotation = FaceDownRotation;
    }
}
