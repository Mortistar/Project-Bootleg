using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKey : MonoBehaviour
{
    [SerializeField] private Sprite sprRed;
    [SerializeField] private Sprite sprBlue;
    [SerializeField] private Sprite sprYellow;

    private Image keyImage;
    public Key.KeyType keyType {get; private set;}
    public void INIT(Key.KeyType _keyType, Transform keyParent)
    {
        keyType = _keyType;
        keyImage = GetComponent<Image>();
        switch(keyType)
        {
            case Key.KeyType.Red:
                keyImage.sprite = sprRed;
                break;
            case Key.KeyType.Blue:
                keyImage.sprite = sprBlue;
                break;
            case Key.KeyType.Yellow:
                keyImage.sprite = sprYellow;
                break;
        }
        transform.parent = keyParent;
        transform.localScale = Vector3.one;
    }
}
