using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersContainer : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<Character> characters;

    public void LoadData(GameData data)
    {
        if (data.Characters.Count > 0)
        {
            characters = data.Characters;
        }
        else
        {
            ResetCharactersRelationships();
        }
    }

    public void SaveData(ref GameData data)
    {
        data.Characters = characters;
    }

    public void ResetCharactersRelationships()
    {
        foreach (Character character in characters)
        {
            character.Relationship = character.InitialRelationship;
            character.Familiar = false;
        }
    }

    public List<Character> GetAllCharacters()
    {
        return characters;
    }
}
