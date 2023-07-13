using System;

public class PlayerData
{
    public readonly string Id;
    public readonly string Name;
    
    public PlayerData(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }
}
