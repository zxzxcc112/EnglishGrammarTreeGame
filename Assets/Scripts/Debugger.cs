using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool mousePosition = false;

    // Update is called once per frame
    void Update()
    {
        if (mousePosition)
        Debug.Log(Input.mousePosition);
    }
}
