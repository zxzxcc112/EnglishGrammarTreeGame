using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    private float displayTime;
    private Canvas dialogBox;
    private float displayTimer;


    private void Awake()
    {
        dialogBox = GetComponentInChildren<Canvas>();
        dialogBox.gameObject.SetActive(false);
        displayTimer = -1.0f;
        displayTime = 4.0f;
    }

    private void Update()
    {
        if(displayTimer >= 0)
        {
            displayTimer -= Time.deltaTime;
            if(displayTimer < 0)
            {
                dialogBox.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        displayTimer = displayTime;
        dialogBox.gameObject.SetActive(true);
    }
}
