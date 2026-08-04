using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float money { get; private set; }
    [SerializeField] private float generateContentButtonPower = 1f;
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    public List<Upgrade> upgrades = new List<Upgrade>();
    public static GameManager Instance;

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
}
