using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SyntaxTreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public static event Action OnNodeDragged;

    public string Text { get { return m_text.text; } set { m_text.text = value; } }

    [SerializeField] private GameObject m_DeleteSelf;
    
    private RectTransform m_rectTransform;
    private TextMeshProUGUI m_text;
    
    public NodeViewer viewer;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_text = GetComponentInChildren<TextMeshProUGUI>();
        viewer = new NodeViewer(m_rectTransform);
    }

    public void Initialize(Vector2 position, string partOfSpeech)
    {
        transform.position = position;
        m_text.text = partOfSpeech;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetInstanceID() != this.gameObject.GetInstanceID())
            return;

        m_DeleteSelf.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetInstanceID() != this.gameObject.GetInstanceID())
            return;

        m_DeleteSelf.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_rectTransform.anchoredPosition += eventData.delta;
        OnNodeDragged?.Invoke();
    }

}
