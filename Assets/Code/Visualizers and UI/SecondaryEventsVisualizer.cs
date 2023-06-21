using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryEventsVisualizer : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [Header("Setups")]
    [SerializeField] private RelationshipInteractionSetup relationshipInteractionPrefab;
    [SerializeField] private CharacteristicInteractionSetup characteristicInteractionPrefab;

    private List<Transform> _interactions = new List<Transform>();

    private EventsManager _eventsManager;

    public RelationshipInteractionSetup RelationshipInteractionPrefab { get => relationshipInteractionPrefab;}
    public CharacteristicInteractionSetup CharacteristicInteractionPrefab { get => characteristicInteractionPrefab; }

    private void Start()
    {
        _eventsManager = GetComponent<EventsManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clear();
            PlayEvent();
        }
    }

    private void Clear()
    {
        foreach (Transform interaction in _interactions)
        {
            Destroy(interaction.gameObject);
        }

        _interactions.Clear();
    }

    private IEnumerator ClearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_interactions.Count > 0)
        {
            Clear();
            PlayEvent();
        }
    }

    public void PlayEvent()
    {
        _eventsManager.PlayEvent();
    }

    public void VisualizeAll()
    {
        float animationDelay = 0.5f;

        foreach (Transform interaction in _interactions)
        {
            SecondaryEvent secondaryEvent = interaction.gameObject.GetComponent<SecondaryEvent>();

            interaction.gameObject.SetActive(true);

            if (secondaryEvent is IAnimatable animatableSecondaryEvent)
            {
                animatableSecondaryEvent.ExecuteAnimation(animationDelay);
                animationDelay += 0.5f;
            }
        }

        StartCoroutine(ClearAfterDelay(5f));
    }

    public void VisualizeInteraction(SecondaryEvent secondaryEvent)
    {
        secondaryEvent.Initialize(_interactions);
    }

    public SecondaryEvent InstantiateEvent(SecondaryEvent prefab)
    {
        SecondaryEvent secondaryEvent = Instantiate(prefab, grid.position, Quaternion.identity);
        secondaryEvent.transform.SetParent(grid);
        return secondaryEvent;
    }

    public bool HasInteractionsToVisualize()
    {
        return _interactions.Count > 0;
    }
}
