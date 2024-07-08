using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ViewNotesUI : MonoBehaviour
{
    public Dropdown tagDropdown;
    public GameObject noteContentPrefab;
    public Transform noteListContainer;
    public NoteManager noteManager;

    void Start()
    {
        tagDropdown.onValueChanged.AddListener(delegate { ShowNotes(); });
        ShowNotes();
    }

    void ShowNotes()
    {
        foreach (Transform child in noteListContainer)
        {
            Destroy(child.gameObject);
        }

        string selectedTag = tagDropdown.options[tagDropdown.value].text;
        var notes = noteManager.Notes.Where(n => n.Tag == selectedTag);

        foreach (var note in notes)
        {
            GameObject noteContent = Instantiate(noteContentPrefab, noteListContainer);
            noteContent.GetComponent<Text>().text = $"{note.Timestamp}: {note.NoteText} ({note.Emotion})";
        }
    }
}
