using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreator
{
    private const string UIPath = "Prefabs/UICanvas";

    public UICanvasView CreateUI()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>(UIPath)).GetComponent<UICanvasView>();
    }
}
