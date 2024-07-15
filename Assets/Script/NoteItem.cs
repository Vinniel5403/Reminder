using UnityEngine;
using TMPro;

public class NoteItem : MonoBehaviour
{
    public TMP_Text dateTimeText;
    public TMP_Text noteText;
    public TMP_Text emotionText;
    public TMP_Text tagText;

    public void SetNoteData(string dateTime, string note, string emotion, string tag)
    {
        if (dateTimeText != null)
        {
            dateTimeText.text = dateTime;
        }

        if (noteText != null)
        {
            noteText.text = note;
        }

        if (emotionText != null)
        {
            emotionText.text = emotion;
        }

        if (tagText != null)
        {
            tagText.text = tag;
        }
    }
}
