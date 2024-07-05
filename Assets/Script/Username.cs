using UnityEngine;
using UnityEngine.UI;

public class UsernameManager : MonoBehaviour
{
    public InputField usernameInputField;
    public Button submitButton;

    private string username;

    void Start()
    {
        // โหลด Username ถ้ามีอยู่ใน PlayerPrefs
        if (PlayerPrefs.HasKey("Username"))
        {
            username = PlayerPrefs.GetString("Username");
            submitButton.GetComponentInChildren<Text>().text = "Username: " + username;
        }
        else
        {
            submitButton.GetComponentInChildren<Text>().text = "Submit Username";
        }

        // เพิ่ม listener ให้กับปุ่ม
        submitButton.onClick.AddListener(SubmitUsername);
    }

    void SubmitUsername()
    {
        // รับค่า Username จาก Input Field
        username = usernameInputField.text;

        // บันทึกค่า Username
        PlayerPrefs.SetString("Username", username);

        // แสดงค่า Username บนปุ่ม
        submitButton.GetComponentInChildren<Text>().text = "Username: " + username;
    }
}
