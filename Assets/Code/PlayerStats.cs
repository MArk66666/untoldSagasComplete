using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacteristicSetup))]
[RequireComponent(typeof(ItemHandler))]
[RequireComponent(typeof(CharactersContainer))]
public class PlayerStats : MonoBehaviour, IDataPersistence
{
    private int _heartsAmount = 3;

    private List<Characteristic> _acquiredCharacteristics = new List<Characteristic>();
    private CharacteristicSetup _characteristicSetup;

    public ItemHandler ItemHandler { get; private set; }
    public CharactersContainer CharactersContainer { get; private set; }

    private void Start()
    {
        _characteristicSetup = GetComponent<CharacteristicSetup>();
        ItemHandler = GetComponent<ItemHandler>();
        CharactersContainer = GetComponent<CharactersContainer>();

        CharactersContainer.ResetCharactersRelationships();
    }
    public void LoadData(GameData data)
    {
        _heartsAmount = data.HeartsAmount;
    }

    public void SaveData(ref GameData data)
    {
        data.HeartsAmount = _heartsAmount;
    }

    public void AddCharacteristic(Characteristic characteristic)
    {
        if (_acquiredCharacteristics.Contains(characteristic)) return;

        _acquiredCharacteristics.Add(characteristic);
        _characteristicSetup.SetupCharacteristic(characteristic);
    }

    public bool CheckForCharacteristic(Characteristic targetCharacteristic)
    {
        foreach (Characteristic characteristic in _acquiredCharacteristics)
        {
            if (characteristic == targetCharacteristic)
                return true;
        }
        return false;
    }

    public void ChangeHeartsAmount(int amount)
    {
        _heartsAmount += amount;
    }

    public void ChangeRelationship(Character character, int amount)
    {
        character.Relationship += amount;
    }
}