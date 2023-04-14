using UnityEngine;

public class UIScaler : MonoBehaviour
{
    //chatgpt
    [SerializeField] private Camera cam; // 要跟隨的相機
    [SerializeField] private Vector2 referenceSize = new Vector2(1920, 1080); // 參考大小

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float height = cam.orthographicSize * 2; // 計算相機的高度
        float width = height * cam.aspect; // 計算相機的寬度

        float widthRatio = width / referenceSize.x; // 計算寬度比例
        float heightRatio = height / referenceSize.y; // 計算高度比例

        float scale = Mathf.Min(widthRatio, heightRatio); // 取寬度和高度比例的最小值作為縮放比例

        rectTransform.localScale = new Vector3(scale, scale, 1); // 設置縮放比例
        rectTransform.sizeDelta = new Vector2(width / scale, height / scale); // 設置大小
    }
}
