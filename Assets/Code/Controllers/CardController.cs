using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private DecisionCard decisionCardPrefab;
    [SerializeField] private Transform cardGrid;

    private List<DecisionCard> _currentCards = new List<DecisionCard>();

    private EventsManager _eventsManager;

    private void Awake()
    {
        _eventsManager = GetComponent<EventsManager>();
    }

    private bool ConditionsMet(Decision decision)
    {
        if (decision.CharacteristicRequirment())
        {
            return _eventsManager.PlayerStats.CheckForCharacteristic(decision.RequiredCharacteristic);
        }

        return true;
    }

    public void ClearCards()
    {
        foreach (DecisionCard card in _currentCards)
        {
            Destroy(card.gameObject);
        }

        if (_currentCards.Count > 0) _currentCards.Clear();
    }

    public void SpawnCards(int currentEventID)
    {
        Chapter currentChapter = _eventsManager.GetCurrentChapter();
        GameEvent currentEvent = currentChapter.GetEvent(currentEventID);

        if (currentEvent.Decisions.Length > 0)
        {
            foreach (Decision decision in currentEvent.Decisions)
            {
                if (!ConditionsMet(decision)) continue;

                DecisionCard decisionCard = Instantiate(decisionCardPrefab.gameObject, cardGrid.position,
                    decisionCardPrefab.transform.rotation).GetComponent<DecisionCard>();

                decisionCard.transform.parent = cardGrid;
                decisionCard.InitializeCard(decision, _eventsManager);

                _currentCards.Add(decisionCard);
            }
        }
    }

    public void RotateAllCardsOnTheScene(DecisionCard exception)
    {
        foreach (DecisionCard card in _currentCards)
        {
            if (card == exception) continue;

            card.StartCoroutine(card.CardTransformHandler.RotateCard(new Vector3(0f, 90f, 0f)));
        }
    }
}
