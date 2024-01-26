using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIHandler : MonoBehaviour
{
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMPro.TextMeshProUGUI bombText;
    [SerializeField] private Transform keyParent;

    [SerializeField] private GameObject keyPrefab;
    private CanvasGroup group;
    private List<UIKey> keys;

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void INIT(PlayerStats stats)
    {
        keys = new List<UIKey>();
        UpdateHealth(stats.health);
        UpdateBombs(stats.bombs);
        ShowUI();
    }
    public void UpdateHealth(float value)
    {
        healthSlider.value = value / 100;
    }
    public void UpdateBombs(float amount)
    {
        bombText.text = "x" + amount.ToString();
    }
    public void ShowUI()
    {
        group.alpha = 1f;
    }
    public void HideUI()
    {
        group.alpha = 0f;
    }
    public bool RemoveKey(Key.KeyType keyType)
    {
        foreach(UIKey key in keys)
        {
            if (key.keyType == keyType)
            {
                keys.Remove(key);
                Destroy(key.gameObject);
                return true;
            }
        }
        return false;
    }
    public void AddKey(Key.KeyType keyType)
    {
        GameObject newKey = Instantiate(keyPrefab);
        UIKey keyScript = newKey.GetComponent<UIKey>();
        keyScript.INIT(keyType, keyParent);
        keys.Add(keyScript);
    }
}
