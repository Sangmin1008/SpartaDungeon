using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField] private PlayerStatHandler statHandler;
    [SerializeField] private float currValue;
    [SerializeField] private float maxValue;
    [SerializeField] private Image uiBar;
    
    private void Start()
    {
        currValue = statHandler.CurrentHealth;
        maxValue = statHandler.CurrentHealth;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        currValue = Mathf.Min(currValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        currValue = Mathf.Max(currValue - amount, 0.0f);
    }

    private float GetPercentage()
    {
        return currValue / maxValue;
    }
}
