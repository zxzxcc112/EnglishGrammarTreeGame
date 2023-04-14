using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerInput : MonoBehaviour
{
    private DefaultInputActions inputActions;

    private PlayerInput playerInput;

    private void Awake()
    {
        inputActions = new DefaultInputActions();
        //inputActions.Player.Move.started += OnMove;
        InputActionMap map = inputActions.Player;

        playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;
    }

    private void Update()
    {
        if (playerInput.actions["move"].IsPressed())
            Debug.Log("player input move is pressed");
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void OnActionChanged(object obj, InputActionChange inputActionChange)
    {
        Debug.Log($"On action change {obj.ToString()} {inputActionChange.ToString()}");
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        Debug.Log($"On move callback {callbackContext.phase}");
    }

    void OnMove(InputValue value)
    {
        Debug.Log("On move value");
    }

    public void OnPoint(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("On point callback");
    }

    public void OnPoint(InputValue value)
    {
        Debug.Log("On point value");
    }
}
