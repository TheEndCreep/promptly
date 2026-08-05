using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float money { get; private set; }
    [SerializeField] private float generateContentButtonPower = 1f;
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    public List<Upgrade> upgrades = new List<Upgrade>();
    public static GameManager Instance;

    [SerializeField] private Button[] buyStateButtons = new Button[3];

    void Awake()
    {
        money = 0f;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        buyStateButtons[0].onClick.AddListener(delegate { ChangeBuyAmount(BuyState.SINGLE); });
        buyStateButtons[1].onClick.AddListener(delegate { ChangeBuyAmount(BuyState.TEN); });
        buyStateButtons[2].onClick.AddListener(delegate { ChangeBuyAmount(BuyState.HUNDRED); });
    }

    private IEnumerator Start()
    {
        while (true)
        {
            float amountToAdd = 0f;
            foreach (Upgrade upgrade in upgrades)
            {
                amountToAdd += upgrade.GetMoneyPerSecond();
            }
            UpdateMoneyAmount(amountToAdd * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GenerateContentButton()
    {
        float amountToAdd = 0f;
        float multiplier = 1f;
        foreach (Upgrade upgrade in upgrades)
        {
            amountToAdd += upgrade.GetMoneyPerClick();
        }
        UpdateMoneyAmount((generateContentButtonPower + amountToAdd) * multiplier);
    }

    public void UpdateMoneyAmount(float amount)
    {
        money += amount;
        moneyDisplay.text = "$" + (Mathf.Round(money * 10f) * 0.1f).ToString();
    }

    public void ChangeBuyAmount(BuyState newState)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.ChangeBuyState(newState);
        }
    }
}
