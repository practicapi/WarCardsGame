using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDeckViewController
{
    private static readonly Quaternion FaceDownRotation = Quaternion.Euler(0,0,180);
    private const float PileVerticalSpaceBetweenCards = 0.0005f;
    private float _deckStartingYPosition;
    
    private Dictionary<string,CardView> _cards;
    private Transform _deckParent;
    private DeckColor _deckColor;

    public void SetDeck(List<CardView> cards, Transform decksParent)
    {
        _deckParent = decksParent;
        _deckStartingYPosition = _deckParent.position.y;
        
        SetCards(cards);
    }

    
    private void SetCards(List<CardView> cards)
    {
        _cards = new Dictionary<string, CardView>();
        
        foreach(var cardView in cards)
        {
            AddCard(cardView);
        }
    }

    public void SetDeckColor(DeckColor deckColor)
    {
        _deckColor = deckColor;
        
        foreach(var cardView in _cards)
        {
            SetCardColorToDeckColor(cardView.Value);
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
            cardView.transform.position = _deckParent.position + (cardsAmount - i + 1) * PileVerticalSpaceBetweenCards * Vector3.up;
            ResetCardRotation(cardView);
        }
    }

    private void MoveDeckOneUp()
    {
        _deckParent.Translate(Vector3.up*PileVerticalSpaceBetweenCards);
    }

    public CardView RemoveCard(string cardId)
    {
        var cardView = _cards[cardId];
        _cards.Remove(cardId);
        return cardView;
    }
    
    public async UniTask MoveCardToDeckBottom(CardView cardView)
    {
        var deckPosition = _deckParent.position;
        await cardView.MoveToPointFacedDown(new Vector3(deckPosition.x, _deckStartingYPosition, deckPosition.z));
        cardView.transform.SetParent(_deckParent);
        MoveDeckOneUp();
    }

    public void AddCard(CardView cardView)
    {
        _cards.Add(cardView.ID, cardView);
        cardView.transform.SetParent(_deckParent);
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
