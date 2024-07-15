using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CSVResetManager : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    private string csvFilePath;

    void Start()
    {
        csvFilePath = Path.Combine(Application.persistentDataPath, "notes.csv");
        resetButton.onClick.AddListener(ResetCSV);
    }

    void ResetCSV()
    {
        if (File.Exists(csvFilePath))
        {
            File.WriteAllText(csvFilePath, string.Empty);
            Debug.Log("CSV file has been reset.");
        }
        else
        {
            Debug.LogError("CSV file not found.");
        }
    }
}
