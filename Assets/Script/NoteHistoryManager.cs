using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class NoteHistoryManager : MonoBehaviour
{
    public TMP_Text dateTimeText; // Reference to the TMP_Text for DateTime
    public TMP_Text noteText; // Reference to the TMP_Text for Note
    public TMP_Text emotionText; // Reference to the TMP_Text for Emotion
    public TMP_Text tagText; // Reference to the TMP_Text for Tag
    public TMP_Dropdown noteOrderDropdown; // Reference to the TMP_Dropdown for Note Order
    private string csvFilePath;
    private string[] lines;

    void Start()
    {
        csvFilePath = Path.Combine(Application.persistentDataPath, "notes.csv");
        LoadNotes();
        PopulateDropdown();
        noteOrderDropdown.onValueChanged.AddListener(delegate { UpdateNoteDisplay(); });
        UpdateNoteDisplay(); // Initial update
    }

    public void LoadNotes()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found");
            return;
        }

        lines = File.ReadAllLines(csvFilePath);
        if (lines.Length == 0)
        {
            Debug.LogWarning("CSV file is empty");
        }
    }

    public void PopulateDropdown()
    {
        noteOrderDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            options.Add($"Latest {i + 1}");
        }

        noteOrderDropdown.AddOptions(options);
    }

    public void UpdateNoteDisplay()
    {
        int noteIndex = noteOrderDropdown.value; // Get selected index from dropdown
        int lineIndex = lines.Length - 1 - noteIndex; // Calculate line index in CSV file

        if (lineIndex < 0 || lineIndex >= lines.Length)
        {
            Debug.LogError("Invalid note index selected");
            return;
        }

        string line = lines[lineIndex];
        string[] columns = line.Split(',');

        if (columns.Length < 4)
        {
            Debug.LogError("Invalid CSV format");
            return;
        }

        string dateTime = columns[0].Trim();
        string note = columns[1].Trim().Trim('"');
        string emotion = columns[2].Trim().Trim('"');
        string tag = columns[3].Trim().Trim('"');

        dateTimeText.text = dateTime;
        noteText.text = note;
        emotionText.text = emotion;
        tagText.text = tag;

        Debug.Log($"Displaying Note: {dateTime}, {note}, {emotion}, {tag}");
    }
}
