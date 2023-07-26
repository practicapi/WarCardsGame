using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsLeftTextController
{
    private CardsLeftTextCreator _creator;
    private CardsLeftTextView _cardsLeftTextView;

    public CardsLeftTextController()
    {
        _creator = new CardsLeftTextCreator();
    }

    public void CreateTextView(Transform parent)
    {
        _cardsLeftTextView = _creator.CreateCardsLeftText();
        var viewTransform = _cardsLeftTextView.transform;
        viewTransform.SetParent(parent);
        viewTransform.localPosition = Vector3.zero;
    }
    
    public void SetColorView(Color color)
    {
        _cardsLeftTextView.SetupView(color);
    }
    
    public void SetNumberView(int number)
    {
        _cardsLeftTextView.SetNumber(number);
    }
}
