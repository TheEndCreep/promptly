using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float money { get; private set; }
    private float totalMoney;
    [SerializeField] private float generateContentButtonPower = 1f;
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    public List<Upgrade> upgrades = new List<Upgrade>();
    public static GameManager Instance;

    [SerializeField] private Button[] buyStateButtons = new Button[3];

    [Header("Unlocks")]
    [SerializeField] private List<int> milestoneThresholds = new List<int>();
    [SerializeField] private List<GameObject> milestoneUnlocks = new List<GameObject>();
    private Dictionary<int, GameObject> milestones = new Dictionary<int, GameObject>();

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
        for (int i = 0; i < milestoneThresholds.Count; i++)
        {
            milestones.Add(milestoneThresholds[i], milestoneUnlocks[i]);
        }

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
        if (amount > 0)
        {
            totalMoney += amount;
        }

        moneyDisplay.text = "$" + (Mathf.Round(money * 10f) * 0.1f).ToString();

        int milestoneCheck = Mathf.RoundToInt(totalMoney);
        if (milestones.ContainsKey(milestoneCheck))
        {
            milestones[milestoneCheck].SetActive(false);
        }
    }

    public void ChangeBuyAmount(BuyState newState)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.ChangeBuyState(newState);
        }
    }
}
