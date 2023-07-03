using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacteristicSetup))]
[RequireComponent(typeof(ItemHandler))]
[RequireComponent(typeof(HeartsVisualizer))]
public class PlayerStats : MonoBehaviour, IDataPersistence
{
    private int _heartsAmount = 3;

    private List<Characteristic> _acquiredCharacteristics = new List<Characteristic>();
    private CharacteristicSetup _characteristicSetup;

    private HeartsVisualizer _heartsVisualizer;

    public ItemHandler ItemHandler { get; private set; }
    private void Start()
    {
        _heartsVisualizer.UpdateHeartGrid(_heartsAmount);
    }

    public void LoadData(GameData data)
    {
        GetAllComponents();

        _heartsAmount = data.HeartsAmount;

        if (data.Characteristics.Count > 0)
        {
            _acquiredCharacteristics = data.Characteristics;

            int lastCharacteristicIndex = _acquiredCharacteristics.Count - 1;
            _characteristicSetup.SetupCharacteristic(_acquiredCharacteristics[lastCharacteristicIndex]);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.HeartsAmount = _heartsAmount;
        data.Characteristics = _acquiredCharacteristics;
    }

    private void GetAllComponents()
    {
        _characteristicSetup = GetComponent<CharacteristicSetup>();
        ItemHandler = GetComponent<ItemHandler>();
        _heartsVisualizer = GetComponent<HeartsVisualizer>();
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
        _heartsVisualizer.UpdateHeartGrid(_heartsAmount);
    }

    public void ChangeRelationship(Character character, int amount)
    {
        character.Relationship += amount;
    }
}