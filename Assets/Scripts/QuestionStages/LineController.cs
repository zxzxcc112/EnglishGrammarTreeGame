using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //負責畫出連接兩點的一條線

    private LineRenderer m_lineRenderer;
    private Transform posStart;
    private Transform posEnd;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 0;

        SyntaxTreeNode.OnNodeDragged += UpdateLine;
    }

    public void Initialize(Transform posStart, Transform posEnd)
    {
        m_lineRenderer.positionCount = 2;
        this.posStart = posStart;
        this.posEnd = posEnd;
        UpdateLine();
    }

    public void UpdateLine()
    {
        if (m_lineRenderer.positionCount != 2) return;
        m_lineRenderer.SetPosition(0, posStart.position);
        m_lineRenderer.SetPosition(1, posEnd.position);
    }

    private void OnDestroy()
    {
        SyntaxTreeNode.OnNodeDragged -= UpdateLine;
    }
}
