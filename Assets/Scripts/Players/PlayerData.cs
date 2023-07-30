using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string _Id;
    [SerializeField] private string _name;
    [SerializeField] private DeckData _deckData;
    [SerializeField] private CardLeftTextData _cardLeftTextData;

    public string Id => _Id;
    public string Name => _name;
    public DeckData DeckData => _deckData;
    public CardLeftTextData CardLeftTextData => _cardLeftTextData;
}

[Serializable]
public class DeckData
{
    [SerializeField] private Texture2D _cardBackFaceTexture;
    public Texture2D CardBackFaceTexture => _cardBackFaceTexture;
}

[Serializable]
public class CardLeftTextData
{
    [SerializeField] private Color _textColor;
    public Color TextColor => _textColor;
}