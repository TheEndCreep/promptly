using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeStats upgradeStats;
    public float amountOwned = 0f;
    public float moneyPerSecondMultiplier = 1f;
    public float moneyPerClickMultiplier = 1f;
    public float pricePerUnitMultiplier = 1.1f;
    private float price;
    private int buyAmount = 1;
    private BuyState buyState;

    [Header("Milestones")]
    [SerializeField] private Dictionary<float, Modifier> milestones = new Dictionary<float, Modifier>();

    [Header("UI")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI amountDisplay;
    [SerializeField] private TextMeshProUGUI priceDisplay;
    [SerializeField] private TextMeshProUGUI moneyPerSecondDisplay;

    void Start()
    {
        price = upgradeStats.BasePrice;
        buyState = BuyState.SINGLE;
        UpdateGUI();
        upgradeButton.onClick.AddListener(PurchaseUpgrade);
        GameManager.Instance.upgrades.Add(this);
    }

    public void PurchaseUpgrade()
    {
        if (GameManager.Instance.money >= price * buyAmount)
        {
            GameManager.Instance.UpdateMoneyAmount(-price * buyAmount);
            amountOwned++;
            price = upgradeStats.BasePrice * Mathf.Pow(pricePerUnitMultiplier, amountOwned);
            UpdateGUI();
        }
    }

    public float GetMoneyPerClick()
    {
        return amountOwned * (upgradeStats.MoneyPerClick * moneyPerClickMultiplier);
    }

    public float GetMoneyPerSecond()
    {
        return amountOwned * (upgradeStats.MoneyPerSecond * moneyPerSecondMultiplier);
    }

    public void ChangeBuyState(BuyState newState)
    {
        buyState = newState;
        if (buyState == BuyState.SINGLE)
        {
            buyAmount = 1;
        }
        else if (buyState == BuyState.TEN)
        {
            buyAmount = 10;
        }
        else if (buyState == BuyState.HUNDRED)
        {
            buyAmount = 100;
        }
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        amountDisplay.text = amountOwned.ToString();
        priceDisplay.text = "$" + ((Mathf.Round(price * 10f) * 0.1f) * buyAmount).ToString();
        float moneyPerSecond = amountOwned * (upgradeStats.MoneyPerSecond * moneyPerSecondMultiplier);
        moneyPerSecondDisplay.text = "$" + (Mathf.Round(moneyPerSecond * 10f) * 0.1f).ToString() + "/sec";
    }
}

public enum BuyState
{
    SINGLE,
    TEN,
    HUNDRED
}
