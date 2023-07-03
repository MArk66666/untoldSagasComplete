using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int HeartsAmount;
    public int CurrentEventID;
    public List<Character> Characters;
    public List<Characteristic> Characteristics;
    public List<Item> Items;
    public Chapter CurrentChapter;
    public TextAsset Language;

    public GameData()
    {
        this.HeartsAmount = 3;
        this.CurrentEventID = 0;
        this.CurrentChapter = null;
        this.Language = null;
        this.Characters = new List<Character>();
        this.Characteristics = new List<Characteristic>();
        this.Items = new List<Item>();
    }
}