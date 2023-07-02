using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager DataPersistence { get; private set; }

    [Header("FIle Storage Config")]
    [SerializeField] private string fileName = "";

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistences;

    private FileDataHandler _dataHandler;

    public TextAsset LocalizationFile { get; private set; }

    private void Awake()
    {
        DataPersistence = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CreateFileDataHandler();
        FindAllDataPersistenceObjects();
    }

    public void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        _dataPersistences = new List<IDataPersistence>(dataPersistences);
    }

    private void CreateFileDataHandler()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void CreateNewGameData()
    {
        _gameData = new GameData();
        SaveGameData();
    }

    public void SaveGameData()
    {
        foreach (IDataPersistence dataPersistence in _dataPersistences)
        {
            dataPersistence.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);
    }

    public void LoadGameData()
    {
        _gameData = _dataHandler.Load();

        if (_gameData == null)
        {
            CreateNewGameData();
        }

        foreach (IDataPersistence dataPersistence in _dataPersistences)
        {
            dataPersistence.LoadData(_gameData);
        }
    }

    public bool HasSavedData()
    {
        GameData data = _dataHandler.Load();
        return data != null;
    }

    public void SetLocalizationFile(TextAsset file)
    {
        LocalizationFile = file;
    }
}
