using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator
{
    private const string BoardPrefabPath = "Prefabs/CasinoItem/Table_Rectangle_00";

    public BoardCreator()
    {
    }
    
    public BoardView CreateBoard()
    {
        var boardGO = GameObject.Instantiate(Resources.Load<GameObject>(BoardPrefabPath));
        return boardGO.GetComponent<BoardView>();
    }
}
