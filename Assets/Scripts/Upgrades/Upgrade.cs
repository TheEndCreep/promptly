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
        UpdateGUI();
        upgradeButton.onClick.AddListener(PurchaseUpgrade);
    }

    public void PurchaseUpgrade()
    {
        if (GameManager.Instance.money >= price)
        {
            if (amountOwned < 1)
            {
                GameManager.Instance.upgrades.Add(this);
            }
            GameManager.Instance.UpdateMoneyAmount(-price);
            amountOwned++;
            price = upgradeStats.BasePrice * Mathf.Pow(pricePerUnitMultiplier, amountOwned);
            if (milestones[amountOwned] != null)
            {

            }
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

    private void UpdateGUI()
    {
        amountDisplay.text = amountOwned.ToString();
        priceDisplay.text = "$" + (Mathf.Round(price * 10f) * 0.1f).ToString();
        float moneyPerSecond = amountOwned * (upgradeStats.MoneyPerSecond * moneyPerSecondMultiplier);
        moneyPerSecondDisplay.text = "$" + (Mathf.Round(moneyPerSecond * 10f) * 0.1f).ToString() + "/sec";
    }
}
