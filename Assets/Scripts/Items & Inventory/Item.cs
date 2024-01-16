using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public string ItemDescription;

    public static implicit operator List<object>(Item v)
    {
        throw new NotImplementedException();
    }
}

