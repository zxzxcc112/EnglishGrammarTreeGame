using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    private RectTransform rectT;
    public TestPlayer player;

    private void Awake()
    {
    }

    private void Start()
    {
        rectT = GetComponent<RectTransform>();
        player.OnHpChanged += ShowHp;
    }


    public void ShowHp(int hp)
    {
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp * 100f);
    }
}
