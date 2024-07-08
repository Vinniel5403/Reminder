using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text savedNotesText;

    void Start()
    {
        // Set listener for the submit button
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        // Load saved notes and display the latest note in UI
        LoadNotes();
    }

    void OnSubmitButtonClick()
    {
        // Get input field data
        string noteText = noteInputField.text;

        // Get current date and time
        string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Save note data in PlayerPrefs
        string savedNotes = PlayerPrefs.GetString("Notes", "");
        string newNote = dateTime + ": " + noteText + "\n";
        savedNotes += newNote;
        PlayerPrefs.SetString("Notes", savedNotes);
        PlayerPrefs.Save();

        // Debug log to console
        Debug.Log("Saved note: " + newNote);

        // Update the latest note in UI
        savedNotesText.text = newNote;

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

        // Display the latest note
        savedNotesText.text = latestNote;
    }
}
