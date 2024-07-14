using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionDisplay : MonoBehaviour
{
    public TMP_Text emotionText;
    public TMP_InputField noteInputField; // Reference to NoteInput field
    public Image noteInputBackground; // Reference to the Image component for the background
    public GameObject MoodPanel; // Reference to the MoodPanel

    void Start()
    {
        // Check if MoodPanel is assigned
        if (MoodPanel == null)
        {
            Debug.LogError("MoodPanel is not assigned in the Inspector");
            return;
        }

        // Get all buttons under the MoodPanel GameObject
        Button[] emotionButtons = MoodPanel.GetComponentsInChildren<Button>();

        // Assign listeners to each button dynamically
        foreach (Button button in emotionButtons)
        {
            button.onClick.AddListener(() => OnEmotionButtonClick(button));
        }

        // Load emotion from PlayerPrefs and display
        LoadEmotion();
    }

    // Method to handle button click events
    public void OnEmotionButtonClick(Button clickedButton)
    {
        // Extract emotion from the clicked button's name
        string emotion = clickedButton.gameObject.name.Replace("Btn", "");
        PlayerPrefs.SetString("Emotion", emotion.ToLower());
        Debug.Log("Sent emotion data: " + emotion.ToLower());
        LoadEmotion(); // Update the displayed emotion

        // Change the color of NoteInput background
        ChangeNoteInputColor(clickedButton);
    }

    // Method to load emotion from PlayerPrefs and display in TMP_Text
    void LoadEmotion()
    {
        string savedEmotion = PlayerPrefs.GetString("Emotion", "None");
        emotionText.text = savedEmotion;
    }

    // Method to change the color of NoteInput background
    void ChangeNoteInputColor(Button clickedButton)
    {
        // Get the color of the clicked button
        Color buttonColor = clickedButton.GetComponent<Image>().color;

        // Set the color of NoteInput background
        if (noteInputBackground != null)
        {
            noteInputBackground.color = buttonColor;
        }
    }
}
