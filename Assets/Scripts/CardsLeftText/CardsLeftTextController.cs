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
        _cardsLeftTextView.transform.SetParent(parent);
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
