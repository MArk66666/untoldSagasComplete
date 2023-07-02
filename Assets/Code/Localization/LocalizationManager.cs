using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour, IDataPersistence
{
    public static LocalizationManager Localization;

    [SerializeField] private TextAsset localizationFile;

    private Dictionary<string, string> translations = new Dictionary<string, string>();

    private void Awake()
    {
        Localization = this;
        //LoadLanguage(DataPersistenceManager.DataPersistence.LocalizationFile);
    }

    public void LoadData(GameData data)
    {
        if (data.Language == null)
        {
            Debug.LogError("Localization file is null or empty!");
            localizationFile = DataPersistenceManager.DataPersistence.LocalizationFile;
            LoadLanguage(localizationFile);
            return;
        }

        localizationFile = data.Language;
        LoadLanguage(localizationFile);
    }

    public void SaveData(ref GameData data)
    {
        data.Language = localizationFile; 
    }

    private void TestOutput()
    {
        string test = GetTranslation("chapter_Birth_event_0_title");
        Debug.Log(test);                                                
    }

    public void LoadLanguage(TextAsset localizationFile)
    {
        string data = localizationFile.text;
        string[] lines = data.Split(new char[] { '\n' });
        translations.Clear();
        foreach (string line in lines)
        {
            int firstCommaIndex = line.IndexOf(',');
            if (firstCommaIndex >= 0 && firstCommaIndex < line.Length - 1)
            {
                string key = line.Substring(0, firstCommaIndex).Trim().Trim('"'); // Remove spaces and quotes.
                string translation = line.Substring(firstCommaIndex + 1).Trim().Trim('"'); // Remove spaces and quotes.
                translations[key] = translation;
            }
        }
    }

    public string GetTranslation(string key)
    {
        if (translations.TryGetValue(key, out string translation))
        {
            return translation;
        }
        else
        {
            if (localizationFile != null)
            {
                LoadLanguage(localizationFile);

                if (translations.TryGetValue(key, out translation))
                {
                    return translation;
                }
            }

            Debug.LogError("No translation found for key: " + key);
            return key;
        }
    }
}

public static class LocalizationKeysHolder
{
    public static string GetEventTitleKey(Chapter chapter, int currentEventID)
    {
        string key = "chapter_" + chapter.name + "_" + "event_" + currentEventID + "_title";
        return key;
    }
}