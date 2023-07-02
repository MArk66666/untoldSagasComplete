using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int HeartsAmount;
    public int CurrentEventID;
    public Chapter CurrentChapter;
    public TextAsset Language;

    public GameData()
    {
        this.HeartsAmount = 3;
        this.CurrentEventID = 0;
        this.CurrentChapter = null;
        Language = null;
    }
}
