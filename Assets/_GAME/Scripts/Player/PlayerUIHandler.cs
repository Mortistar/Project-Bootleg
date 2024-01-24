using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIHandler : MonoBehaviour
{
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMPro.TextMeshProUGUI bombText;
    private CanvasGroup group;
    

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void INIT(PlayerStats stats)
    {
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
}
