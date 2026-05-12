using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPauseAction;

    private SurivalistInputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new SurivalistInputActions();
        inputActions.Survivalist.Enable();

        inputActions.Survivalist.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        inputActions.Survivalist.Pause.performed -= Pause_performed;

        inputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Survivalist.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
}
