using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BattleStateService BattleStateService { get; private set; }
    public BattleLogicService BattleLogicService { get; private set; }
    public PlayerController Player1Controller { get; private set; }
    public PlayerController Player2Controller { get; private set; }
    public BoardController BoardController { get; private set; }
    public CardsDeckDataCreator CardsDeckDataCreator { get; private set; }
    public TurnsService TurnsService { get; private set; }
    public DataLoaderService DataLoaderService { get; private set; } 
    public UIController UIController { get; private set; }
    
    void Awake()
    {
        SetupSingleton();
        InitiateControllers();
        SetData();
        CreateObjectsView();
        PrepareObjectsView();
    }

    public void Update() // todo move to a new class
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            new SpaceKeyPressedCommand().Execute().Forget();
        }
    }

    private void SetupSingleton()
    {
        Instance = this;
    }

    private void SetData()
    {
        var fullCardsDeck = CardsDeckDataCreator.CreateFullShuffledDeck();
        var numberOfPlayers = 2;
        var numberOfCardsForEachPlayer = fullCardsDeck.Length / numberOfPlayers;
        var player1Data = DataLoaderService.Load<PlayerData>("Player1Data");
        Player1Controller.SetData(player1Data, fullCardsDeck.Take(numberOfCardsForEachPlayer));
        var player2Data = DataLoaderService.Load<PlayerData>("Player2Data");
        Player2Controller.SetData(player2Data, fullCardsDeck.Skip(numberOfCardsForEachPlayer));
        TurnsService.SetData(Player1Controller.PlayerId);
    }

    private void CreateObjectsView()
    {
        BoardController.CreateBoardView();
        Player1Controller.CreateViews();
        Player2Controller.CreateViews();
        UIController.CreateUI();
    }

    private void PrepareObjectsView()
    {
        new PrepareObjectsViewCommand().Execute().Forget();
    }

    private void InitiateControllers()
    {
        CardsDeckDataCreator = new CardsDeckDataCreator();
        Player1Controller = new PlayerController();
        Player2Controller = new PlayerController();
        BoardController = new BoardController();
        TurnsService = new TurnsService();
        BattleLogicService = new BattleLogicService();
        BattleStateService = new BattleStateService();
        DataLoaderService = new DataLoaderService();
        UIController = new UIController();
    }
}
