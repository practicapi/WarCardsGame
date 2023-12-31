using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDrawnPileViewController
{
    private const float HorizontalOffsetBetweenCards = 0.05f;
    private const float VerticalOffsetBetweenCards = 0.001f;
    
    private Stack<CardView> _cardsDrawnPile;
    private Transform _pileParent;
    
    public CardsDrawnPileViewController()
    {
        _cardsDrawnPile = new Stack<CardView>();
    }

    public void CreatePile(Transform parent)
    {
        _pileParent = parent;
    }

    public void AddCardToPile(CardView cardView)
    {
        _cardsDrawnPile.Push(cardView);
    }

    public async UniTask AnimateDrawCardToPile(CardView cardView, bool shouldFaceUp = true)
    {
        var cardPositionOffset = CalculateCardPositionOffset();
        var destinationPoint = _pileParent.position + cardPositionOffset;
        
        await cardView.JumpToPoint(destinationPoint, shouldFaceUp);
        cardView.transform.SetParent(_pileParent);
    }

    private Vector3 CalculateCardPositionOffset()
    {
        var index = _cardsDrawnPile.Count;
        var horizontalOffsetBetweenCards = HorizontalOffsetBetweenCards * (index-1) * _pileParent.right;
        var verticalOffsetBetweenCards = VerticalOffsetBetweenCards * index * _pileParent.up;
        
        return horizontalOffsetBetweenCards + verticalOffsetBetweenCards;
    }

    public CardView RemoveCardFromPile()
    {
        var cardView = _cardsDrawnPile.Pop();
        cardView.transform.SetParent(null);
        return cardView;
    }

    public Stack<CardView> GetDrawnCards()
    {
        return _cardsDrawnPile;
    }
    
    public void SetPileRotationAngleView(float angle)
    {
        _pileParent.localRotation = angle.ToQuaternionAroundYAxis();
    }

    public void RevealAllCardsInDrawnPile()
    {
        foreach (var cardView in _cardsDrawnPile)
        {
            cardView.FaceUp();
        }
    }
}