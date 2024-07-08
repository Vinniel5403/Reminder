using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button happyButton;
    public Button boringButton;
    public Button exciteButton;
    public Button angryButton;
    public Button nervousButton;
    public Button sadButton;

    private void Start()
    {
        // Add listeners to the buttons
        happyButton.onClick.AddListener(OnHappyBtnClick);
        boringButton.onClick.AddListener(OnBoringBtnClick);
        exciteButton.onClick.AddListener(OnExciteBtnClick);
        angryButton.onClick.AddListener(OnAngryBtnClick);
        nervousButton.onClick.AddListener(OnNervousBtnClick);
        sadButton.onClick.AddListener(OnSadBtnClick);
    }
    public void OnHappyBtnClick()
    {
        SetEmotion("Happy");
    }

    public void OnBoringBtnClick()
    {
        SetEmotion("Boring");
    }

    public void OnExciteBtnClick()
    {
        SetEmotion("Excite");
    }

    public void OnAngryBtnClick()
    {
        SetEmotion("Angry");
    }

    public void OnNervousBtnClick()
    {
        SetEmotion("Nervous");
    }

    public void OnSadBtnClick()
    {
        SetEmotion("Sad");
    }

    private void SetEmotion(string emotion)
    {
        PlayerPrefs.SetString("Emotion", emotion.ToLower());
        Debug.Log("Sent emotion data: " + emotion.ToLower());
    }
}
