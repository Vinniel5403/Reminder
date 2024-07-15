using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionStatsManager : MonoBehaviour
{
    public TMP_Text mostCommonEmotionText; // TextMeshPro for displaying most common emotion
    public TMP_Text mostCommonTagText; // TextMeshPro for displaying most common tag
    public Image emotionImage; // Image for displaying the emotion
    public Sprite happySprite; // Sprite for happy emotion
    public Sprite boringSprite; // Sprite for boring emotion
    public Sprite exciteSprite; // Sprite for excite emotion
    public Sprite angrySprite; // Sprite for angry emotion
    public Sprite nervousSprite; // Sprite for nervous emotion
    public Sprite sadSprite; // Sprite for sad emotion

    private string csvFilePath;

    void Start()
    {
        csvFilePath = Path.Combine(Application.persistentDataPath, "notes.csv");
        CalculateStats();
    }

    void CalculateStats()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found");
            return;
        }

        Dictionary<string, int> emotionsCount = new Dictionary<string, int>();
        Dictionary<string, int> tagsCount = new Dictionary<string, int>();

        string[] lines = File.ReadAllLines(csvFilePath);

        foreach (string line in lines)
        {
            string[] columns = line.Split(',');
            if (columns.Length < 4) continue;

            string emotion = columns[2].Trim().Trim('"').ToLower();
            string tag = columns[3].Trim().Trim('"').ToLower();

            if (!emotionsCount.ContainsKey(emotion))
            {
                emotionsCount[emotion] = 0;
            }
            emotionsCount[emotion]++;

            if (!tagsCount.ContainsKey(tag))
            {
                tagsCount[tag] = 0;
            }
            tagsCount[tag]++;
        }

        string mostCommonEmotion = GetMostCommon(emotionsCount);
        string mostCommonTag = GetMostCommon(tagsCount);

        mostCommonEmotionText.text = $"{mostCommonEmotion}";
        mostCommonTagText.text = $"{mostCommonTag}";

        // เปลี่ยนสีของข้อความและภาพตามอารมณ์
        ChangeTextColorAndImage(mostCommonEmotion);

        Debug.Log($"Most Common Emotion: {mostCommonEmotion}");
        Debug.Log($"Most Common Tag: {mostCommonTag}");
    }

    string GetMostCommon(Dictionary<string, int> countDict)
    {
        int maxCount = 0;
        string mostCommon = "None";

        foreach (var item in countDict)
        {
            if (item.Value > maxCount)
            {
                maxCount = item.Value;
                mostCommon = item.Key;
            }
        }

        return mostCommon;
    }

    void ChangeTextColorAndImage(string emotion)
    {
        Color color;
        Sprite sprite;
        switch (emotion.ToLower())
        {
            case "happy":
                color = new Color(0f, 1f, 0f); // เขียวสด
                sprite = happySprite;
                break;
            case "boring":
                color = new Color(1f, 0.8f, 0f); // เหลืองเข้ม
                sprite = boringSprite;
                break;
            case "excite":
                color = new Color(1f, 0.5f, 0f); // ส้มสด
                sprite = exciteSprite;
                break;
            case "angry":
                color = new Color(1f, 0f, 0f); // แดงสด
                sprite = angrySprite;
                break;
            case "nervous":
                color = new Color(0.5f, 0f, 1f); // ม่วงสด
                sprite = nervousSprite;
                break;
            case "sad":
                color = new Color(0f, 0f, 1f); // น้ำเงินสด
                sprite = sadSprite;
                break;
            default:
                color = Color.white;
                sprite = null;
                break;
        }

        mostCommonEmotionText.color = color;
        mostCommonTagText.color = color;
        emotionImage.sprite = sprite;
    }
}
