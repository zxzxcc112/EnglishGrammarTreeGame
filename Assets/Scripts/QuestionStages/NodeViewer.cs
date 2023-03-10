using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeViewer
{
    public static float MarginX = 25f;
    public static float Step = -120f;

    public float SpaceSize { get; set; }

    private RectTransform m_rect;

    public NodeViewer(RectTransform rect)
    {
        m_rect = rect;
        SpaceSize = m_rect.rect.width + MarginX * 2;
    }

}
