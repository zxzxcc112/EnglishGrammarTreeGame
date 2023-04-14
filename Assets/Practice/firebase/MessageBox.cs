using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    private TMP_Text messageText;

    private void Awake()
    {
        messageText = GetComponentInChildren<TMP_Text>();
    }

    public void SetMessage(string message)
    {
        messageText.text = message;
    }

    private void OnDisable()
    {
        messageText.text = string.Empty;
    }
}
