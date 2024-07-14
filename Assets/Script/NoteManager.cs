using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button resetButton; // Add reference to reset button
    [SerializeField] private TMP_Text savedNotesText;
    private string csvFilePath;

    void Start()
    {
        // Set listener for the submit button
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        // Set listener for the reset button
        resetButton.onClick.AddListener(ResetData);

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

        // Save note data in PlayerPrefs
        string savedNotes = PlayerPrefs.GetString("Notes", "");
        string newNote = dateTime + ", " + noteText + "\n";
        savedNotes += newNote;
        PlayerPrefs.SetString("Notes", savedNotes);
        PlayerPrefs.Save();

        // Debug log to console
        Debug.Log("Saved note: " + newNote);

        // Update the latest note in UI
        UpdateSavedNotesText(dateTime, noteText, latestEmotion);

        // Save data to CSV
        SaveToCSV(dateTime, noteText, latestEmotion);

        // Clear input field
        noteInputField.text = "";
    }

    void LoadNotes()
    {
        // Load notes from PlayerPrefs
        string savedNotes = PlayerPrefs.GetString("Notes", "");

        // Get the latest note
        string[] notesArray = savedNotes.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        string latestNote = notesArray.Length > 0 ? notesArray[notesArray.Length - 1] : "";

        // Extract date and note text
        string[] latestNoteParts = latestNote.Split(new[] { ", " }, 2, System.StringSplitOptions.None);
        if (latestNoteParts.Length == 2)
        {
            // Load the latest emotion from PlayerPrefs
            string latestEmotion = PlayerPrefs.GetString("Emotion", "None");
            UpdateSavedNotesText(latestNoteParts[0], latestNoteParts[1], latestEmotion);
        }
    }

    void UpdateSavedNotesText(string dateTime, string noteText, string emotion)
    {
        // Display the latest note along with the emotion
        savedNotesText.text = $"{dateTime}, {noteText}, {emotion}";
    }

    void SaveToCSV(string dateTime, string noteText, string emotion)
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
            writer.WriteLine($"{dateTime}, \"{noteText}\", \"{emotion}\"");
        }

        // Debug log to console
        Debug.Log($"Saved to CSV: {dateTime}, {noteText}, {emotion}");
    }

    void ResetData()
    {
        // Clear PlayerPrefs
        PlayerPrefs.DeleteAll();

        // Clear CSV file content
        if (File.Exists(csvFilePath))
        {
            File.WriteAllText(csvFilePath, string.Empty);
        }

        // Clear the UI text
        savedNotesText.text = "";

        // Debug log to console
        Debug.Log("Data has been reset.");
    }
}
