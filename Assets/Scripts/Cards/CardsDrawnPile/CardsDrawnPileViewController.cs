using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDrawnPileViewController
{
    private const float HorizontalOffsetBetweenCards = 0.01f;
    private const float VerticalOffsetBetweenCards = 0.001f;
    
    private Stack<CardView> _cardsDrawnPile;
    private Transform _pileParent;
    
    public CardsDrawnPileViewController()
    {
        _cardsDrawnPile = new Stack<CardView>();
    }

    public void CreatePile(Transform parent)
    {
        _pileParent = new GameObject("PileParent").transform;
        _pileParent.SetParent(parent);
    }
    
    public void SetPilePosition(Vector3 position)
    {
        _pileParent.transform.position = position;
    }
    
    public void AddCardToPile(CardView cardView)
    {
        _cardsDrawnPile.Push(cardView);
    }

    public async UniTask DrawCardToPile(CardView cardView, bool shouldFaceUp = true)
    {
        var cardPositionOffset = CalculateCardPositionOffset();
        var destinationPoint = _pileParent.position + cardPositionOffset;
        
        await cardView.JumpToPoint(destinationPoint, shouldFaceUp);
        cardView.transform.SetParent(_pileParent);
    }

    private Vector3 CalculateCardPositionOffset()
    {
        var index = _cardsDrawnPile.Count - 1;
        var horizontalOffsetBetweenCards = HorizontalOffsetBetweenCards * index * _pileParent.right;
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
}