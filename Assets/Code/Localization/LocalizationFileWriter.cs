using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LocalizationFileWriter : MonoBehaviour
{
    [SerializeField] private string fileName = "English.csv";  // The name of the CSV file.
    [SerializeField] private List<Chapter> chapters;  // Reference to your Chapters.

    [ContextMenu("WriteEventsToCSV")]
    public void WriteEventsToCSV()
    {
        StringBuilder csv = new StringBuilder();

        // Loop over each Chapter.
        for (int i = 0; i < chapters.Count; i++)
        {
            Chapter chapter = chapters[i];
            // Loop over each GameEvent in this Chapter.
            for (int j = 0; j < chapter.events.Length; j++)
            {
                GameEvent gameEvent = chapter.GetEvent(j);
                string key = "chapter_" + chapter.name + "_event_" + gameEvent.GetEventID() + "_title";  // Create a key for this event.
                string translation = gameEvent.Title;  // Get the title of the event.
                csv.AppendLine("\"" + key + "\",\"" + translation + "\"");  // Append this line to the CSV.
            }
        }

        string path = Path.Combine(Application.dataPath, fileName);  // The path to the CSV file.
        File.WriteAllText(path, csv.ToString());  // Write the CSV to the file.
    }
}