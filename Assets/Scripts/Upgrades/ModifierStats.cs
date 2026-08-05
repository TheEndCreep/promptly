using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierStats", menuName = "Scriptable Objects/ModifierStats")]
public class ModifierStats : ScriptableObject
{
    public String Name;
    public int id;
    public float AdditiveModifier;
    public float Multiplier;
    public float Price;
}
