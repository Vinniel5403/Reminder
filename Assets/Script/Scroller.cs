using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RectTransform canvasRect;
    public Image uiImage;
    public InputField pageNumberInput;

    // ความสูงของแต่ละหน้าใน Canvas (หาร 6 เพื่อหาความสูงของแต่ละหน้า)
    float pageHeight;

    void Start()
    {
        // หาความสูงของแต่ละหน้า
        pageHeight = canvasRect.sizeDelta.y / 6f;
    }

    public void ScrollToPageNumber() // เปลี่ยนชื่อฟังก์ชันเป็น ScrollToPageNumber
    {
        // รับค่าหมายเลขหน้าจาก InputField
        int pageNumber = int.Parse(pageNumberInput.text);

        // ตรวจสอบหน้าที่ถูกต้อง
        if (pageNumber >= 1 && pageNumber <= 6)
        {
            // หาค่า y ที่ต้องการเลื่อนไป
            float targetY = -pageNumber * pageHeight;

            // ตั้งค่า anchoredPosition ของ UI Image
            Vector2 anchoredPos = uiImage.rectTransform.anchoredPosition;
            anchoredPos.y = targetY;
            uiImage.rectTransform.anchoredPosition = anchoredPos;
        }
        else
        {
            Debug.LogError("หมายเลขหน้าไม่ถูกต้อง");
        }
    }
}
