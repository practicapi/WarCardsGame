using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator
{
    private const string BoardPrefabPath = "Prefabs/TableBoard";

    public BoardView CreateBoard()
    {
        var boardGO = GameObject.Instantiate(Resources.Load<GameObject>(BoardPrefabPath));
        return boardGO.GetComponent<BoardView>();
    }
}
