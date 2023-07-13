using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BattleLogicService BattleLogicService { get; private set; }
    public PlayerController Player1Controller { get; private set; }
    public PlayerController Player2Controller { get; private set; }
    public BoardController BoardController { get; private set; }
    public CardsDeckDataCreator CardsDeckDataCreator { get; private set; }
    public TurnsService TurnsService { get; private set; }
    
    void Awake()
    {
        SetupSingleton();
        InitiateControllers();
        SetData();
        CreateObjectsView();
        PrepareObjectsView();
        StartGameLogic();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            new SpaceKeyPressedCommand().Execute();
        }
    }

    private void StartGameLogic()
    {
        
    }

    private void SetupSingleton()
    {
        Instance = this;
    }

    private void SetData()
    {
        var fullCardsDeck = CardsDeckDataCreator.CreateFullShuffledDeck();
        var halfDeckSize = fullCardsDeck.Length / 2;
        Player1Controller.SetData("Josh", DeckColor.Blue, fullCardsDeck.Take(halfDeckSize));
        Player2Controller.SetData("Tom", DeckColor.Red, fullCardsDeck.Skip(halfDeckSize));
        TurnsService.SetData(Player1Controller.PlayerId);
    }

    private void CreateObjectsView()
    {
        BoardController.CreateBoardView();
        Player1Controller.CreateCardsView();
        Player2Controller.CreateCardsView();
    }

    private void PrepareObjectsView()
    {
        new PrepareObjectsViewCommand().Execute();
    }

    private void InitiateControllers()
    {
        CardsDeckDataCreator = new CardsDeckDataCreator();
        Player1Controller = new PlayerController();
        Player2Controller = new PlayerController();
        BoardController = new BoardController();
        TurnsService = new TurnsService();
        BattleLogicService = new BattleLogicService();
    }
}
