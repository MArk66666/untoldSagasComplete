using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsVisualizer : MonoBehaviour
{
    [SerializeField] private Transform characterInformationPanel;
    [SerializeField] private Transform charactersGrid;
    [SerializeField] private Button characterButtonPrefab;
    [SerializeField] private CharactersContainer charactersContainer;
    [SerializeField] private SecondaryEventsVisualizer secondaryEventsVisualizer;

    private List<Transform> _visualizedCharacterButtons = new List<Transform>();
    private RelationshipInteractionSetup _currentVisualizedCharacter;

    public void UpdateCharactersGrid()
    {
        foreach(Transform button in _visualizedCharacterButtons)
        {
            Destroy(button.gameObject);
        }

        _visualizedCharacterButtons.Clear();

        List<Character> characters = charactersContainer.GetAllCharacters();
        foreach (Character character in characters)
        {
            if (!character.Familiar)
            {
                continue;
            }

            Button button = Instantiate(characterButtonPrefab, charactersGrid);
            button.image.sprite = character.Icon;
            button.onClick.AddListener(() => VisualizeCharacter(character));
            _visualizedCharacterButtons.Add(button.transform);
        }
    }

    public void VisualizeCharacter(Character character)
    {
        if (_currentVisualizedCharacter != null)
            Destroy(_currentVisualizedCharacter.gameObject);

        RelationshipInteractionSetup prefab = secondaryEventsVisualizer.FullSizeCharacterInteractionPrefab;
        RelationshipInteractionSetup relationshipInteraction = secondaryEventsVisualizer.InstantiateEvent(prefab) as RelationshipInteractionSetup;

        relationshipInteraction.transform.SetParent(characterInformationPanel);
        relationshipInteraction.transform.position = characterInformationPanel.position;
        relationshipInteraction.gameObject.SetActive(true);

        relationshipInteraction.SetCharacter(character);
        relationshipInteraction.SetTargetValue(character.Relationship);

        _currentVisualizedCharacter = relationshipInteraction;

        if (relationshipInteraction is IAnimatable animatableInteraction)
            animatableInteraction.ExecuteAnimation();
    }
}
