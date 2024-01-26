using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key
{
    public enum KeyType
    {
        Red,
        Blue,
        Yellow
    }
    public KeyType keyType {get; private set;}
    public Key(KeyType _keyType)
    {
        keyType = _keyType;
    }

}
