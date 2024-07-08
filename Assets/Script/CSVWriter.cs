using UnityEngine;
using System.Collections;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    private string filePath = "notes.csv"; // ตั้งชื่อไฟล์ CSV ที่จะบันทึกข้อมูล
    private string delimiter = ","; // ตัวคั่นข้อมูลใน CSV

    public void WriteToCSV(string dateTime, string emotion, string noteText, string tag)
    {
        string line = dateTime + delimiter + emotion + delimiter + noteText + delimiter + tag + "\n";

        // เขียนข้อมูลลงไฟล์ CSV
        File.AppendAllText(filePath, line);

        Debug.Log("บันทึกข้อมูลเรียบร้อยแล้ว: " + line);
    }
}
