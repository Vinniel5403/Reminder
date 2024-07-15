using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text savedNotesText;
    [SerializeField] private TMP_Dropdown tagDropdown; // Reference to the Tag Dropdown
    [SerializeField] private NoteHistoryManager noteHistoryManager; // Reference to NoteHistoryManager
    private string csvFilePath;

    void Start()
    {
        // Set listener for the submit button
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        // Set the file path for the CSV file
        csvFilePath = Path.Combine(Application.persistentDataPath, "notes.csv");

        // Debug log to console
        Debug.Log("CSV File Path: " + csvFilePath);

        // Load saved notes and display the latest note in UI
        LoadNotes();
    }

    void OnSubmitButtonClick()
    {
        // Get input field data
        string noteText = noteInputField.text;

        // Get current date and time
        string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Get the latest emotion from PlayerPrefs
        string latestEmotion = PlayerPrefs.GetString("Emotion", "None");

        // Get selected tag from dropdown
        string selectedTag = tagDropdown.options[tagDropdown.value].text;

        // Save note data in PlayerPrefs
        string savedNotes = PlayerPrefs.GetString("Notes", "");
        string newNote = dateTime + ", " + noteText + ", " + latestEmotion + ", " + selectedTag + "\n";
        savedNotes += newNote;
        PlayerPrefs.SetString("Notes", savedNotes);
        PlayerPrefs.Save();

        // Debug log to console
        Debug.Log("Saved note: " + newNote);

        // Update the latest note in UI
        UpdateSavedNotesText(dateTime, noteText, latestEmotion, selectedTag);

        // Save data to CSV
        SaveToCSV(dateTime, noteText, latestEmotion, selectedTag);

        // Clear input field
        noteInputField.text = "";

        // Update Note History Manager
        noteHistoryManager.LoadNotes();
        noteHistoryManager.PopulateDropdown();
        noteHistoryManager.UpdateNoteDisplay();
    }

    void LoadNotes()
    {
        // Load notes from PlayerPrefs
        string savedNotes = PlayerPrefs.GetString("Notes", "");

        // Get the latest note
        string[] notesArray = savedNotes.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        string latestNote = notesArray.Length > 0 ? notesArray[notesArray.Length - 1] : "";

        // Extract date and note text
        string[] latestNoteParts = latestNote.Split(new[] { ", " }, System.StringSplitOptions.None);
        if (latestNoteParts.Length >= 3)
        {
            // Load the latest emotion from PlayerPrefs
            string latestEmotion = latestNoteParts[latestNoteParts.Length - 2];
            string latestTag = latestNoteParts[latestNoteParts.Length - 1];
            UpdateSavedNotesText(latestNoteParts[0], latestNoteParts[1], latestEmotion, latestTag);
        }
    }

    void UpdateSavedNotesText(string dateTime, string noteText, string emotion, string tag)
    {
        // Display the latest note along with the emotion and tag
        savedNotesText.text = $"{dateTime}, {noteText}, {emotion}, {tag}";
    }

    void SaveToCSV(string dateTime, string noteText, string emotion, string tag)
    {
        // Check if the directory exists, if not, create it
        string directoryPath = Path.GetDirectoryName(csvFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Create or open the file
        using (StreamWriter writer = new StreamWriter(csvFilePath, true))
        {
            // Write the data to the CSV file
            writer.WriteLine($"{dateTime}, \"{noteText}\", \"{emotion}\", \"{tag}\"");
        }

        // Debug log to console
        Debug.Log($"Saved to CSV: {dateTime}, {noteText}, {emotion}, {tag}");
    }
}
