using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    public abstract UniTask Execute();
}
public abstract class BaseCommand<T>
{
    protected GameManager _gm;
    protected T _data;
    
    protected BaseCommand(T data)
    {
        _data = data;
        _gm = GameManager.Instance;
    }

    public abstract UniTask Execute();
}