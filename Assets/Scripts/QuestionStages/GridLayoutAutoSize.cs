using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutAutoSize : MonoBehaviour
{
    private GridLayoutGroup gridLayout;
    private RectTransform rectTransform;

    private ushort rowCell;
    private ushort colCell;
    private bool isSquare;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        colCell = 3;
    }

    public void SetCellSize()
    {
        Vector2 newCellSize = new Vector2(rectTransform.rect.width / colCell, rectTransform.rect.width / colCell);
        gridLayout.cellSize = newCellSize;
    }


}
