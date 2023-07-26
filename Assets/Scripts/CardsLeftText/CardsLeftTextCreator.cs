using UnityEngine;

public class CardsLeftTextCreator
{
    private const string CardsLeftTextPath = "Prefabs/CardsLeftText";

    public CardsLeftTextView CreateCardsLeftText()
    {
        var cardsLeftTextGO = GameObject.Instantiate(Resources.Load<GameObject>(CardsLeftTextPath));
        return cardsLeftTextGO.GetComponent<CardsLeftTextView>();
    }
}
