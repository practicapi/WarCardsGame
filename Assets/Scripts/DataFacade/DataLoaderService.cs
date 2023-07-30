using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoaderService
{
    public T Load<T>(string name) where T : ScriptableObject
    {
        return Resources.Load<T>("ScriptableObjects/" + name);
    }
}
