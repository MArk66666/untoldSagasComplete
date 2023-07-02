using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersContainer : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    public void ResetCharactersRelationships()
    {
        foreach (Character character in characters)
        {
            character.Relationship = character.InitialRelationship;
        }
    }

    public List<Character> GetAllCharacters()
    {
        return characters;
    }
}
