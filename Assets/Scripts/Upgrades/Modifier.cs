using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Modifier : MonoBehaviour
{
    [SerializeField] private ModifierStats modifierStats;

    [Header("GUI")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI priceDisplay;
    [SerializeField] private TextMeshProUGUI descriptionDisplay;

    void Awake()
    {
        purchaseButton.onClick.AddListener(PurchaseModifier);
        purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = modifierStats.Name;
        priceDisplay.text = "$" + modifierStats.Price;
        descriptionDisplay.text = modifierStats.Description;
    }

    public void PurchaseModifier()
    {
        if (GameManager.Instance.money >= modifierStats.Price)
        {
            GameManager.Instance.UpdateMoneyAmount(-modifierStats.Price);
            foreach (Upgrade upgrade in GameManager.Instance.upgrades)
            {
                if (upgrade.upgradeStats.upgradeType == modifierStats.AffectedUpgrade)
                {
                    upgrade.moneyPerSecondMultiplier *= modifierStats.Multiplier;
                    upgrade.UpdateGUI();
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void UnlockModifier()
    {
        Debug.Log("Modifier " + name + " unlocked");
    }
}
