using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextChangeWhenOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float offset = 5f;
    [SerializeField] private Transform m_tmp;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_tmp.position += Vector3.down * offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_tmp.position += Vector3.up * offset;
    }
}
