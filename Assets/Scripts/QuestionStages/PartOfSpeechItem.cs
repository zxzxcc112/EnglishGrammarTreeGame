using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartOfSpeechItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //�@�ӥi�즲���~�A�Φr��ӥN�����
    //���b�S�w�ϰ�i�H���ͤ@�ӬۦP���ʪ��𪺸`�I(�ѯS�w�ϰ�B�z)
    
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
        
        //�p�G�w�g���Q������node�A���i��]�w
        if (DetectedParent == DefaultDetectedParent)
            DetectedParent = collision.transform;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SyntaxTreeNode>(out SyntaxTreeNode node) == false)
            return;

        //��Q������node���}�ɡA�p�G�٦���Lnode�b�d�򤺡A�Nnode�]�w���Q����
        if (DetectedParent == DefaultDetectedParent)
            DetectedParent = node.transform;

        //���h��node�b�d�򤺡A��Z���̵u��
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
        //�p�G���}�d��node���O������H�A���ݭn�]�^�w�]
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
        //TODO: �ݭn�ץ����P��ҮɡA���ʶZ�������T
        m_rectTransform.anchoredPosition += eventData.delta;
        //m_rectTransform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        m_rectTransform.SetParent(m_origParent);

        m_image.raycastTarget = true;
    }
}
