using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    [SerializeField] private Image mask;
    private float originalSize;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float size)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * size);
    }
}
