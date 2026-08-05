using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierStats", menuName = "Scriptable Objects/ModifierStats")]
public class ModifierStats : ScriptableObject
{
    public String Name;
    public UpgradeType AffectedUpgrade = UpgradeType.NONE;
    public int id;
    public String Description;
    public float Multiplier = 1f;
    public float Price = 0f;
    [Header("Synergy Enhancements")]
    public UpgradeType RequiredUpgrade = UpgradeType.NONE;
    public int UpgradeAmountRequired = 0;
    public float multiplierPerAmount = 1f;
}
