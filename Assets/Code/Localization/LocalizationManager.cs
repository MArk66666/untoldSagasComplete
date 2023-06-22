using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public TextAsset csvFile;  // You can set this in the Unity Inspector.

    private Dictionary<string, string> translations = new Dictionary<string, string>();

    private void Start()
    {
        LoadLanguage();
    }

    public void LoadLanguage()
    {
        string data = csvFile.text;
        string[] lines = data.Split(new char[] { '\n' });
        translations.Clear();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                string key = parts[0].Trim().Trim('"'); // Remove spaces and quotes.
                string translation = parts[1].Trim().Trim('"'); // Remove spaces and quotes.
                translations[key] = translation;

                Debug.Log("Loaded key: " + key); // Print out each key that is loaded.
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
            Debug.LogError("No translation found for key: " + key);
            return key;
        }
    }
}