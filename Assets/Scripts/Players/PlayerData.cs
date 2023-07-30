using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string _Id;
    [SerializeField] private string _name;
    [SerializeField] private DeckData _deckData;
    [SerializeField] private Color _color;

    public string Id => _Id;
    public string Name => _name;
    public DeckData DeckData => _deckData;
    public Color Color => _color;
}

[Serializable]
public class DeckData
{
    [SerializeField] private Texture2D _cardBackFaceTexture;
    public Texture2D CardBackFaceTexture => _cardBackFaceTexture;
}