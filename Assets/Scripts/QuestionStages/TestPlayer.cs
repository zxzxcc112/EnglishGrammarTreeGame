using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public event Action<int> OnHpChanged;

    public int hp = 5;

    public void PlusHp()
    {
        hp++;
        OnHpChanged?.Invoke(hp);
    }

    public void ReduceHp()
    {
        hp--;
        OnHpChanged?.Invoke(hp);
    }
}
