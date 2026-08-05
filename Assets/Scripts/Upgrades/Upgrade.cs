using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Upgrade : MonoBehaviour
{
    public UpgradeStats upgradeStats;
    public float amountOwned = 0f;
    public float moneyPerSecondMultiplier = 1f;
    public float moneyPerClickMultiplier = 1f;
    public float pricePerUnitMultiplier = 1.1f;
    private float price;
    private int buyAmount = 1;
    private BuyState buyState;

    [Header("Milestones")]
    [SerializeField] private List<int> milestoneAmount = new List<int>();
    [SerializeField] private List<Modifier> milestoneUnlocks = new List<Modifier>();
    private Dictionary<float, Modifier> milestones = new Dictionary<float, Modifier>();

    [Header("UI")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI amountDisplay;
    [SerializeField] private TextMeshProUGUI priceDisplay;
    [SerializeField] private TextMeshProUGUI moneyPerSecondDisplay;

    void Start()
    {
        for (int i = 0; i < milestoneAmount.Count; i++)
        {
            milestones.Add(milestoneAmount[i], milestoneUnlocks[i]);
        }

        price = upgradeStats.BasePrice;
        buyState = BuyState.SINGLE;
        UpdateGUI();
        upgradeButton.onClick.AddListener(PurchaseUpgrade);
        GameManager.Instance.upgrades.Add(this);
    }

    public void PurchaseUpgrade()
    {
        if (GameManager.Instance.money >= price)
        {
            GameManager.Instance.UpdateMoneyAmount(-price);
            amountOwned += buyAmount;
            float newPrice = 0f;
            for (int i = 0; i < buyAmount; i++)
            {
                newPrice += upgradeStats.BasePrice * Mathf.Pow(pricePerUnitMultiplier, amountOwned + i);
            }
            price = newPrice;

            if (milestones.ContainsKey(amountOwned))
            {
                milestones[amountOwned].gameObject.SetActive(true);
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
        float newPrice = 0f;
        for (int i = 0; i < buyAmount; i++)
        {
            newPrice += upgradeStats.BasePrice * Mathf.Pow(pricePerUnitMultiplier, amountOwned + i);
        }
        price = newPrice;
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        amountDisplay.text = amountOwned.ToString();
        priceDisplay.text = "$" + ((Mathf.Round(price * 10f) * 0.1f)).ToString();
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

public enum UpgradeType
{
    NONE,
    TOKEN,
    EMPLOYEE,
    SERVER
}
