using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BattleLogicService BattleLogicService { get; private set; }
    public PlayerController PlayerOneController { get; private set; }
    public PlayerController PlayerTwoController { get; private set; }
    public BoardController Board { get; private set; }
    public CardsDeckDataCreator CardsDeckDataCreator { get; private set; }
    void Awake()
    {
        Instance = this;
        InitiateControllers();
        SetupControllers();
        CreateObjects();
        BeginGame();
    }

    private void SetupControllers()
    {
        var fullCardsDeck = CardsDeckDataCreator.CreateFullShuffledDeck();
        PlayerOneController.SetData("Josh", DeckColor.Blue, fullCardsDeck.Take(fullCardsDeck.Length/2));
        PlayerTwoController.SetData("Tom", DeckColor.Red, fullCardsDeck);
    }

    private void CreateObjects()
    {
        Board.CreateBoard();
        PlayerOneController.CreateCardsView();
        PlayerTwoController.CreateCardsView();
    }

    private void BeginGame()
    {
        PlayerOneController.PileUpCardsDeckView();
        PlayerOneController.SetDeckPositionView(Board.GetBoardSurfaceCenter());
        
        PlayerTwoController.PileUpCardsDeckView();
        PlayerTwoController.SetDeckPositionView(Board.GetBoardSurfaceCenter());
    }

    private void InitiateControllers()
    {
        CardsDeckDataCreator = new CardsDeckDataCreator();
        PlayerOneController = new PlayerController();
        PlayerTwoController = new PlayerController();
        Board = new BoardController();
    }
}
