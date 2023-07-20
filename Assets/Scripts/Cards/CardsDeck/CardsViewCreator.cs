using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsViewCreator
{
    private const string CardTextureFormat = @"PlayingCards_{0}{1}_00_col";
    private const string CardPrefab = @"Blank_PlayingCards_Blank_00";

    public CardsViewCreator()
    {
    }
    
    public List<CardView> CreateCards(CardData[] cardsData, out GameObject deckParent)
    {
        deckParent = new GameObject("Deck");
        deckParent.transform.position = Vector3.zero;
        
        var cardsViews = new List<CardView>();

        foreach (var cardData in cardsData)
        {
            var suitName = cardData.Suit.ToString();
            var texturePath = "Textures/" + string.Format(CardTextureFormat, suitName, GetNumberAsTwoDigitsString(cardData.Value));
            var cardTexture = Resources.Load<Texture2D>(texturePath);
            var cardGOPath = "Prefabs/" + CardPrefab;
            var cardGO = GameObject.Instantiate(Resources.Load<GameObject>(cardGOPath), deckParent.transform, true);
            cardGO.name = "Card_" + cardData.Value + "_" + suitName;
            cardGO.transform.position = Vector3.zero;
            cardGO.GetComponent<MeshRenderer>().material.SetTexture("_MainTex2", cardTexture);
            var cardView = cardGO.GetComponent<CardView>();
            cardView.Setup(cardData.Id);
            cardsViews.Add(cardView);
        }
        
        return cardsViews;
    }

    private string GetNumberAsTwoDigitsString(int number)
    {
        return number > 9 ? number.ToString() : "0" + number;
    }
}

public enum DeckColor
{
    Red,
    Blue
}
public enum CardSuit
{
    Club=0,
    Heart=1,
    Diamond=2,
    Spade=3
}