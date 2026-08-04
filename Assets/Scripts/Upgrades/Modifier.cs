using System;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    public String name;

    public void UnlockModifier()
    {
        Debug.Log("Modifier " + name + " unlocked");
    }
}
