using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacteristicInteractable
{
    void AddCharacteristic(Characteristic characteristic);
    bool CheckForCharacteristic(Characteristic characteristic);
}

[RequireComponent(typeof(CharacteristicSetup))]
public class PlayerStats : MonoBehaviour, ICharacteristicInteractable
{
    private int _heartsAmount = 3;
    private List<Characteristic> _acquiredCharacteristics = new List<Characteristic>();

    private CharacteristicSetup _characteristicSetup;

    private void Start()
    {
        _characteristicSetup = GetComponent<CharacteristicSetup>();
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