using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemTest : MonoBehaviour
{
    public struct MyContext { }
    public UnityEvent<MyContext> unityEvent;

    private void Update()
    {
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {

    }

    public void OnMovement2(MyContext callbackContext)
    {

    }

}
