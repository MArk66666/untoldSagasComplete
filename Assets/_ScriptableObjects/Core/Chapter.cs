using UnityEngine;
using CustomInspector;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Chapter", menuName = "Custom/New Chapter")]

public class Chapter : ScriptableObject
{
    public GameEvent[] events;

    private void OnValidate()
    {
        UpdateIdentifiers();
    }

    private void UpdateIdentifiers()
    {
        for (int i = 0; i < events.Length; i++)
        {
            GameEvent gameEvent = events[i];
            gameEvent.AssignEventID(i);
        }
    }

    public GameEvent GetEvent(int id)
    {
        return events[id];
    }
}

[System.Serializable]
public class GameEvent
{
    [TextArea(2, 5)]
    public string Title = "";

    public Sprite Background = null;

    public AudioClip Ambience = null;

    public Decision[] Decisions;

    public bool Dialogue;

    [ShowIf("Dialogue")]
    public Character DialogueCharacter;

    public bool ChangeMusicStyle;

    [ShowIf("ChangeMusicStyle")]
    public MusicType MusicStyle = MusicType.Default;

    public enum MusicType
    {
        Default, Adventure, Dramatic
    }

    [SerializeField, ReadOnly] private int _eventID;

    public void AssignEventID(int sequenceNumber)
    {
        _eventID = sequenceNumber;
    }

    public int GetEventID()
    {
        return _eventID;
    }
}

[System.Serializable]
public class Decision
{
    public string Title;

    [TextArea(5, 10)]
    public string Description;

    public AudioClip ClickSound;

    [Preview(Size.medium)]
    public Sprite Image;

    public Requirment[] Requirments;
    public Interaction[] Interactions;

    public int TargetEventID;

    public bool Played { get; set; }

    [ShowIf("CharacteristicRequirment")] public Characteristic RequiredCharacteristic;
    [ShowIf("ItemRequirment")] public Item RequiredItem;

    [ShowIf("ChangeChapter")] public Chapter TargetChapter;
    [ShowIf("AddCharacteristic")] public Characteristic AddableCharacteristic;
    [ShowIf("AddItem")] public Item AddableItem;
    [ShowIf("RemoveItem")] public Item RemovableItem;
    [ShowIf("ChangeRelationship")] public CharacterRelationshipModifier CharactersRelationshipModifier;

    public enum Interaction
    {
        AddHeart, RemoveHeart, AddCharacteristic, ChangeRelationship, AddItem, RemoveItem, PlayChapter
    }

    public enum Requirment
    {
        CharacteristicRequirment, ItemRequirment
    }

    public bool CharacteristicRequirment()
    {
        for (int i = 0; i < Requirments.Length; i++)
        {
            if (Requirments[i] == Requirment.CharacteristicRequirment)
                return true;
        }
        return false;
    }
    public bool ItemRequirment()
    {
        for (int i = 0; i < Requirments.Length; i++)
        {
            if (Requirments[i] == Requirment.ItemRequirment)
                return true;
        }
        return false;
    }
    public bool ChangeChapter()
    {
        for(int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i] == Interaction.PlayChapter)
                return true;
        }

        return false;
    }
    public bool AddCharacteristic()
    {
        for (int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i] == Interaction.AddCharacteristic)
                return true;
        }

        return false;
    }
    public bool AddItem()
    {
        for (int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i] == Interaction.AddItem)
                return true;
        }

        return false;
    }
    public bool RemoveItem()
    {
        for (int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i] == Interaction.RemoveItem)
                return true;
        }

        return false;
    }
    public bool ChangeRelationship()
    {
        for (int i = 0; i < Interactions.Length; i++)
        {
            if (Interactions[i] == Interaction.ChangeRelationship)
                return true;
        }

        return false;
    }
}

[System.Serializable]
public class CharacterRelationshipModifier
{
    public RelationshipModifier[] RelationshipModifiers;

    [System.Serializable]
    public class RelationshipModifier
    {
        public Character Character;
        public int Relationship;
    }
}