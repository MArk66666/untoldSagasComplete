using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneComponentsHolder))]
[RequireComponent(typeof(CardController))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(SecondaryEventsVisualizer))]
public class EventsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Chapter initialChapter;

    private int _currentEventID = 0;
    private Chapter _currentChapter;

    public SceneComponentsHolder SceneComponents { get; private set; }
    public CardController CardController { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    public SecondaryEventsVisualizer SecondaryEventsVisualizer { get; private set; }
    public DataPersistenceManager DataPersistenceManager { get; private set; }

    private void Awake()
    {
        SetChapter(initialChapter);

        DataPersistenceManager = GameObject.FindAnyObjectByType<DataPersistenceManager>();

        if (DataPersistenceManager != null)
        {
            DataPersistenceManager.FindAllDataPersistenceObjects();
            DataPersistenceManager.LoadGameData();

            //DataPersistenceManager.SaveGameData();
        }
    }

    private void Start()
    {
        SceneComponents = GetComponent<SceneComponentsHolder>();
        CardController = GetComponent<CardController>();
        PlayerStats = GetComponent<PlayerStats>();
        SecondaryEventsVisualizer = GetComponent<SecondaryEventsVisualizer>();

        PlayEvent();
    }

    public void LoadData(GameData data)
    {
        if (data.CurrentChapter != null)
        {
            _currentChapter = data.CurrentChapter;
        }

        _currentEventID = data.CurrentEventID;
    }

    public void SaveData(ref GameData data)
    {
        data.CurrentChapter = _currentChapter;
        data.CurrentEventID = _currentEventID;
    }

    public void PlayEvent()
    {
        CardController.ClearCards();

        if (SecondaryEventsVisualizer.HasInteractionsToVisualize())
        {
            SecondaryEventsVisualizer.VisualizeAll();
            return;
        }

        CardController.SpawnCards(_currentEventID);
        SceneComponents.SetUpScene(_currentEventID);

        if (DataPersistenceManager != null)
            DataPersistenceManager.SaveGameData();
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