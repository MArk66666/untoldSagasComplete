using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] localizationFiles;

    private Dictionary<string, string> _translations;
    private string _currentLanguage = "Russian";

    public void InitializeLocalization(string language)
    {
        _translations = new Dictionary<string, string>();

        TextAsset languageFile = null;

        foreach (TextAsset file in localizationFiles)
        {
            if (file.name == language)
            {
                languageFile = file;
            }
        }

        if (languageFile == null)
        {
            Debug.LogError("No localization file found");
            return;
        }

        _currentLanguage = language;

        string[] lines = languageFile.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            _translations[values[0]] = values[1];
        }
    }

    public string GetTranslation(string key)
    {
        if (_translations.ContainsKey(key))
        {
            return _translations[key];
        }

        return $"[Missing translation: {key} {_currentLanguage}]";
    }
}
