using UnityEngine;

public class UIScaler : MonoBehaviour
{
    //chatgpt
    [SerializeField] private Camera cam; // �n���H���۾�
    [SerializeField] private Vector2 referenceSize = new Vector2(1920, 1080); // �ѦҤj�p

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float height = cam.orthographicSize * 2; // �p��۾�������
        float width = height * cam.aspect; // �p��۾����e��

        float widthRatio = width / referenceSize.x; // �p��e�פ��
        float heightRatio = height / referenceSize.y; // �p�Ⱚ�פ��

        float scale = Mathf.Min(widthRatio, heightRatio); // ���e�שM���פ�Ҫ��̤p�ȧ@���Y����

        rectTransform.localScale = new Vector3(scale, scale, 1); // �]�m�Y����
        rectTransform.sizeDelta = new Vector2(width / scale, height / scale); // �]�m�j�p
    }
}
