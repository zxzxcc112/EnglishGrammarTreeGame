using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActive : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");

    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");

    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");

    }
}
