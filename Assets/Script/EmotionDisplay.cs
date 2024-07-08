using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionDisplay : MonoBehaviour
{
    public TMP_Text emotionText;

    void Start()
    {
        // Find the MoodPanel GameObject
        GameObject moodPanel = GameObject.Find("MoodPanel");

        // Get all buttons under the MoodPanel GameObject
        Button[] emotionButtons = moodPanel.GetComponentsInChildren<Button>();

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
    }

    // Method to load emotion from PlayerPrefs and display in TMP_Text
    void LoadEmotion()
    {
        string savedEmotion = PlayerPrefs.GetString("Emotion", "None");
        emotionText.text = savedEmotion;
    }
}