using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartOfSpeechItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //一個可拖曳物品，用字串來代表詞性
    //當放在特定區域可以產生一個相同詞性的樹的節點(由特定區域處理)
    
    public string Text { get { return m_TMP.text; } set { m_TMP.text = value; } }

    private TextMeshProUGUI m_TMP;
    
    //for detect node
    public Transform DetectedParent { get; private set; }
    public static Transform DefaultDetectedParent { get; private set; }

    private static LineRenderer m_detectLine;


    //for Drag
    private RectTransform m_rectTransform;
    private Transform m_origParent;
    private Image m_image;


    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_image = GetComponent<Image>();

        m_detectLine = GameObject.Find("DetectedLine").GetComponent<LineRenderer>();
        DefaultDetectedParent = GameObject.Find("SyntaxTree").transform;
        DetectedParent = DefaultDetectedParent;

        m_TMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Debug.Log(DetectedParent == m_defaultDetectedParent);
    }



    //----------------------------------------------
    //Detect node method
    //----------------------------------------------
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SyntaxTreeNode>(out SyntaxTreeNode node) == false)
            return;
        
        //如果已經有被偵測的node，不進行設定
        if (DetectedParent == DefaultDetectedParent)
            DetectedParent = collision.transform;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SyntaxTreeNode>(out SyntaxTreeNode node) == false)
            return;

        //當被偵測的node離開時，如果還有其他node在範圍內，將node設定為被偵測
        if (DetectedParent == DefaultDetectedParent)
            DetectedParent = node.transform;

        //當有多個node在範圍內，找距離最短的
        if (node.gameObject != DetectedParent.gameObject &&
            Vector3.Distance(transform.position, node.transform.position) < 
            Vector3.Distance(transform.position, DetectedParent.position))
        {
            DetectedParent = node.transform;
        }

        Debug.DrawLine(this.transform.position, DetectedParent.position);

        m_detectLine.positionCount = 2;
        m_detectLine.SetPosition(0, this.transform.position);
        m_detectLine.SetPosition(1, DetectedParent.position);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //如果離開範圍的node不是偵測對象，不需要設回預設
        if (DetectedParent == collision.transform)
        {
            DetectedParent = DefaultDetectedParent;
            m_detectLine.positionCount = 0;
        }
    }

    //----------------------------------------------
    //Drag method
    //----------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_origParent = m_rectTransform.parent;
        m_rectTransform.SetParent(m_rectTransform.GetComponentInParent<Canvas>().transform);
        m_rectTransform.SetAsLastSibling();

        m_image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //TODO: 需要修正不同比例時，移動距離不正確
        m_rectTransform.anchoredPosition += eventData.delta;
        //m_rectTransform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        m_rectTransform.SetParent(m_origParent);

        m_image.raycastTarget = true;
    }
}
