using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneComponentsHolder))]
[RequireComponent(typeof(CardController))]
[RequireComponent(typeof(PlayerStats))]
public class EventsManager : MonoBehaviour
{
    [SerializeField] private Chapter initialChapter;

    private int _currentEventID = 0;
    private Chapter _currentChapter;

    public SceneComponentsHolder SceneComponents { get; private set; }
    public CardController CardController { get; private set; }
    public PlayerStats PlayerStats { get; private set; }

    private void Start()
    {
        SetChapter(initialChapter);

        SceneComponents = GetComponent<SceneComponentsHolder>();
        CardController = GetComponent<CardController>();
        PlayerStats = GetComponent<PlayerStats>();

        PlayEvent();
    }

    public void PlayEvent()
    {
        CardController.ClearCards();
        CardController.SpawnCards(_currentEventID);
        SceneComponents.SetUpScene(_currentEventID);
    }

    public void SetEventID(int id)
    {
        _currentEventID = id;
    }

    public void SetChapter(Chapter chapter)
    {
        _currentChapter = chapter;
    }

    public Chapter GetCurrentChapter()
    {
        return _currentChapter;
    }
}