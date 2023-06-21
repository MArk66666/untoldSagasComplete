using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/New Character")]
public class Character : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public int InitialRelationship;

    public int Relationship { get; set; }
}
