using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristic", menuName = "Custom/New Characteristic")]
public class Characteristic : ScriptableObject
{
    public string Name;
    public Sprite Icon;
}
