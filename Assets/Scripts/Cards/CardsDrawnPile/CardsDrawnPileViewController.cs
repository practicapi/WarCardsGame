using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardsDrawnPileViewController
{
    private const float HorizontalOffsetBetweenCards = 0.01f;
    
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
        var destinationPoint = _pileParent.position + _pileParent.right * HorizontalOffsetBetweenCards * _cardsDrawnPile.Count;
        await cardView.JumpToPoint(destinationPoint, shouldFaceUp);
        cardView.transform.SetParent(_pileParent);
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
}