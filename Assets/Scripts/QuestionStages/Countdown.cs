using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private Image m_image;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    public void UpdateCountdown(float amount)
    {
        m_image.fillAmount = amount;
    }
}
