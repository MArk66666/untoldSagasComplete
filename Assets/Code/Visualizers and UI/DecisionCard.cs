using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CardTransformHandler))]
public class DecisionCard : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject frontSide;
    [SerializeField] private Button OnClickButton;
    [SerializeField] private RequiredCharacteristicSetup requiredCharacteristic;
    [SerializeField] private Image requiredItemIcon;

    private int _targetID = 0;
    private AudioSource _source;
    private Decision.Interaction[] _interactions;
    private Chapter _targetChapter;
    private Characteristic _addableCharacteristic;
    private CharacterRelationshipModifier.RelationshipModifier[] _relationshipModifiers;
    private Item _addableItem;
    private Item _removableItem;

    private EventsManager _eventsManager;
    private PlayerStats _playerStats;
    private SecondaryEventsVisualizer _secondaryEventsVisualizer;

    public CardTransformHandler CardTransformHandler { get; private set; }
    public bool ChangeChapter { get; private set; }

    private void Awake()
    {
        OnClickButton.onClick.AddListener(SelectCard);

        _source = GetComponent<AudioSource>();
        CardTransformHandler = GetComponent<CardTransformHandler>();

        requiredCharacteristic.SetActive(false);
        requiredItemIcon.gameObject.SetActive(false);
        StartCoroutine(CardTransformHandler.RotateCard(new Vector3(0f, 0f, 0f)));
    }

    private void Update()
    {
        CardTransformHandler.ChangeScale();
    }

    private void SetValues(string title, string description, int id)
    {
        titleText.text = title;
        descriptionText.text = description;
        _targetID = id;
    }

    private void SetConditions(Decision.Interaction[] interactions)
    {
        _interactions = interactions;        
    }

    private void SetClickSound(AudioClip clip)
    {
        _source.clip = clip;
    }

    private void SetTargetChapter(Chapter chapter)
    {
        _targetChapter = chapter;
        ChangeChapter = true;

        ResetTargetID();
    }

    private void SetEventsManager(EventsManager eventsManager)
    {
        _eventsManager = eventsManager;
        _playerStats = _eventsManager.PlayerStats;
        _secondaryEventsVisualizer = _eventsManager.SecondaryEventsVisualizer;
    }

    private void SetCharacteristicToAdd(Characteristic characteristic)
    {
        _addableCharacteristic = characteristic;
    }

    private void SetItemToAdd(Item item)
    {
        _addableItem = item;
    }

    private void SetItemToRemove(Item item)
    {
        _removableItem = item;
    }

    private void SetCharactersRelationModifier(CharacterRelationshipModifier charactersRelationshipModifier)
    {
        _relationshipModifiers = charactersRelationshipModifier.RelationshipModifiers;    
    }

    private void SetRequiredCharacteristic(Characteristic characteristic)
    {
        requiredCharacteristic.SetupCharacteristic(characteristic);
        requiredCharacteristic.SetActive(true);
        requiredCharacteristic.VisualizeIcon();
    }

    private void SetRequiredItem(Item item)
    {
        requiredItemIcon.gameObject.SetActive(true);
        requiredItemIcon.sprite = item.Icon;
    }

    private void SelectCard()
    {
        if (!CardTransformHandler.Used)
        {
            PlaySound();

            Vector3 targetRotation = new Vector3(0f, 90f, 0f);
            StartCoroutine(CardTransformHandler.RotateCard(targetRotation, true));
            ApplyInteractions();

            _eventsManager.CardController.RotateAllCardsOnTheScene(this);
        }
        else
        {
            Vector3 targetRotation = new Vector3(0f, 90f, 0f);
            StartCoroutine(CardTransformHandler.RotateCard(targetRotation));

            SetClickSound(_eventsManager.SceneComponents.GetDefaultClickSound());
            PlaySound();
        }
    }

    private void PlaySound()
    {
        _source.Play();
    }

    private void ApplyInteractions()
    {
        for (int i = 0; i < _interactions.Length; i++)
        {
            Decision.Interaction currentInteraction = _interactions[i];
            switch (currentInteraction)
            {
                case Decision.Interaction.AddHeart:
                    _playerStats.ChangeHeartsAmount(+1);
                    break;
                case Decision.Interaction.RemoveHeart:
                    _playerStats.ChangeHeartsAmount(-1);
                    break;
                case Decision.Interaction.AddCharacteristic:
                    _playerStats.AddCharacteristic(_addableCharacteristic);
                    VisualizeAddableCharacteristic(_addableCharacteristic);
                    break;
                case Decision.Interaction.ChangeRelationship:
                    ChangeRelationship();
                    break;
                case Decision.Interaction.AddItem:
                    _playerStats.ItemHandler.AddItem(_addableItem);
                    break;
                case Decision.Interaction.RemoveItem:
                    _playerStats.ItemHandler.RemoveItem(_removableItem);
                    break;
                case Decision.Interaction.PlayChapter:
                    EditChapter();
                    break;
            }
        }
    }

    private void VisualizeAddableCharacteristic(Characteristic characteristic)
    {
        CharacteristicInteractionSetup prefab = _secondaryEventsVisualizer.CharacteristicInteractionPrefab;
        SecondaryEvent secondaryEvent = _secondaryEventsVisualizer.InstantiateEvent(prefab);
        CharacteristicInteractionSetup characteristicInteraction = secondaryEvent as CharacteristicInteractionSetup;

        characteristicInteraction.SetCharacteristic(characteristic);
        _secondaryEventsVisualizer.VisualizeInteraction(secondaryEvent);
    }

    private void ChangeRelationship()
    {
        if (_relationshipModifiers.Length <= 0) return;

        for (int i = 0; i < _relationshipModifiers.Length; i++)
        {
            Character targetCharacter = _relationshipModifiers[i].Character;
            int amount = _relationshipModifiers[i].Relationship;

            RelationshipInteractionSetup prefab = _secondaryEventsVisualizer.RelationshipInteractionPrefab;
            SecondaryEvent secondaryEvent = _secondaryEventsVisualizer.InstantiateEvent(prefab);
            RelationshipInteractionSetup relationshipInteraction = secondaryEvent as RelationshipInteractionSetup;

            relationshipInteraction.SetCharacter(targetCharacter);
            relationshipInteraction.SetTargetValue(targetCharacter.Relationship + amount);
            _secondaryEventsVisualizer.VisualizeInteraction(secondaryEvent);       

            _playerStats.ChangeRelationship(targetCharacter, amount);
        }
    }

    private void ResetTargetID()
    {
        _targetID = 0;
    }

    private void CheckForAdditionalModifiers(Decision decision)
    {
        if (decision.ChangeChapter())
            SetTargetChapter(decision.TargetChapter);

        if (decision.AddCharacteristic())
            SetCharacteristicToAdd(decision.AddableCharacteristic);

        if (decision.ChangeRelationship())
            SetCharactersRelationModifier(decision.CharactersRelationshipModifier);

        if (decision.CharacteristicRequirment())
            SetRequiredCharacteristic(decision.RequiredCharacteristic);

        if (decision.ItemRequirment())
            SetRequiredItem(decision.RequiredItem);

        if (decision.AddItem())
            SetItemToAdd(decision.AddableItem);

        if (decision.RemoveItem())
            SetItemToRemove(decision.RemovableItem);
    }

    public void PlayEvent()
    {
        _eventsManager.SetEventID(_targetID);
        _eventsManager.PlayEvent();
    }

    public void EditChapter()
    {
        _eventsManager.SetChapter(_targetChapter);
    }
                                                                                                           
    public void InitializeCard(Decision decision, EventsManager eventsManager)
    {
        SetValues(decision.Title, decision.Description, decision.TargetEventID);
        SetEventsManager(eventsManager);
        SetConditions(decision.Interactions);
        CheckForAdditionalModifiers(decision);

        AudioClip defaultClip = _eventsManager.SceneComponents.GetDefaultClickSound();
        AudioClip clip = decision.ClickSound == null ? defaultClip : decision.ClickSound;

        SetClickSound(clip);
    }
}