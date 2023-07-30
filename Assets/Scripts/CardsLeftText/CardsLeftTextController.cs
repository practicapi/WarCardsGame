using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsLeftTextController
{
    private CardsLeftTextCreator _creator;
    private CardsLeftTextView _cardsLeftTextView;
    private CardLeftTextData _data;

    public CardsLeftTextController()
    {
        _creator = new CardsLeftTextCreator();
    }
    
    public void SetData(CardLeftTextData playerDataCardLeftTextData)
    {
        _data = playerDataCardLeftTextData;
    }
    
    public void CreateTextView(Transform parent)
    {
        _cardsLeftTextView = _creator.CreateCardsLeftText();
        var viewTransform = _cardsLeftTextView.transform;
        viewTransform.SetParent(parent);
        viewTransform.localPosition = Vector3.zero;
    }
    
    public void SetupView()
    {
        _cardsLeftTextView.SetupView(_data.TextColor);
    }
    
    public void SetNumberView(int number)
    {
        _cardsLeftTextView.SetNumber(number);
    }
}
