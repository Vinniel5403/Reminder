using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using TMPro;

public class EmotionStatsManager : MonoBehaviour
{
    public TMP_Text yesterdayEmotionText; // TextMeshPro for displaying yesterday's emotion
    public TMP_Text last7DaysEmotionText; // TextMeshPro for displaying last 7 days emotion
    private string csvFilePath;

    void Start()
    {
        csvFilePath = Path.Combine(Application.persistentDataPath, "notes.csv");
        CalculateEmotions();
    }

    void CalculateEmotions()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found");
            return;
        }

        Dictionary<string, int> yesterdayEmotions = new Dictionary<string, int>();
        Dictionary<string, int> last7DaysEmotions = new Dictionary<string, int>();
        DateTime today = DateTime.Today;
        DateTime yesterday = today.AddDays(-1);
        DateTime weekAgo = today.AddDays(-7);

        string[] lines = File.ReadAllLines(csvFilePath);

        foreach (string line in lines)
        {
            string[] columns = line.Split(',');
            if (columns.Length < 3) continue;

            DateTime date = DateTime.ParseExact(columns[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string emotion = columns[2].Trim().ToLower();

            if (date.Date == yesterday.Date)
            {
                if (!yesterdayEmotions.ContainsKey(emotion))
                {
                    yesterdayEmotions[emotion] = 0;
                }
                yesterdayEmotions[emotion]++;
            }

            if (date.Date >= weekAgo.Date && date.Date < today.Date)
            {
                if (!last7DaysEmotions.ContainsKey(emotion))
                {
                    last7DaysEmotions[emotion] = 0;
                }
                last7DaysEmotions[emotion]++;
            }
        }

        string mostCommonYesterday = GetMostCommonEmotion(yesterdayEmotions);
        string mostCommonLast7Days = GetMostCommonEmotion(last7DaysEmotions);

        yesterdayEmotionText.text = $"Yesterday's most common emotion: {mostCommonYesterday}";
        last7DaysEmotionText.text = $"Most common emotion in last 7 days: {mostCommonLast7Days}";
    }

    string GetMostCommonEmotion(Dictionary<string, int> emotions)
    {
        int maxCount = 0;
        string mostCommonEmotion = "None";

        foreach (var emotion in emotions)
        {
            if (emotion.Value > maxCount)
            {
                maxCount = emotion.Value;
                mostCommonEmotion = emotion.Key;
            }
        }

        return mostCommonEmotion;
    }
}
