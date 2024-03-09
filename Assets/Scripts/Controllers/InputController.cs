using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController
{
    private InputCallback inputCallback;
    private PlayerInput playerInput;

    public InputCallback InputCallback => inputCallback;

    public InputController(PlayerInput playerInput)
    {
        inputCallback = new InputCallback();
        this.playerInput = playerInput;

        AddListeners();
    }

    public void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        playerInput.actions["SwapWeapon"].performed += OnSwapButtonPressed;
        //playerInput.actions["Fire"].performed += OnFireButtonPressed;
    }

    private void RemoveListeners()
    {
        playerInput.actions["SwapWeapon"].performed -= OnSwapButtonPressed;
        //playerInput.actions["Fire"].performed -= OnFireButtonPressed;
    }

    private void OnFireButtonPressed(InputAction.CallbackContext obj)
    {
        inputCallback.OnFireButtonPressed(obj.ReadValueAsButton());
    }

    private void OnSwapButtonPressed(InputAction.CallbackContext obj)
    {
        inputCallback.OnSwapButtonPressed(obj.ReadValueAsButton());
    }

    public Vector2 GetMoveDirection()
    {
        return playerInput.actions["Move"].ReadValue<Vector2>();
    }

    public Vector2 GetTurnDirection()
    {
        return playerInput.actions["Rotate"].ReadValue<Vector2>();
    }

    public void Update()
    {
        inputCallback.OnFireButtonPressed(GetTurnDirection().sqrMagnitude > 0.9f);
    }

}
