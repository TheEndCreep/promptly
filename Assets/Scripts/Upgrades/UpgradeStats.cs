using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UpgradeStats", menuName = "Scriptable Objects/UpgradeStats")]
public class UpgradeStats : ScriptableObject
{
    public String Name;
    public UpgradeType upgradeType;
    public int id;
    public float MoneyPerSecond;
    public float MoneyPerClick;
    public float BasePrice;
}
