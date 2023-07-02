using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/New Character")]
public class Character : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    [TextArea(3, 10)]
    public string Description;
    public int InitialRelationship;

    public int Relationship { get; set; }
    public bool Familiar { get; set; } 
}
