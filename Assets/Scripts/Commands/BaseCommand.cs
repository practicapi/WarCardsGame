using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCommand
{
    protected GameManager _gm;

    protected BaseCommand()
    {
        _gm = GameManager.Instance;
        Init();
    }

    protected abstract void Init();
    public abstract void Execute();
}
