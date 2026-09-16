using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    PlayerInputActions playerInputActions;
    public void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_Performed;
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // if (OnInteractAction != null)
        // {
        //     OnInteractAction(this, EventArgs.Empty);
        // }
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        // Vector2 axis = new Vector2(0, 0);
        Vector2 axis = playerInputActions.Player.Move.ReadValue<Vector2>();

        // playerInputActions.Player.Move.ReadValue<Vector2>();

        // if (Input.GetKey(KeyCode.W))
        //     axis.x = +1.0f;
        // if (Input.GetKey(KeyCode.S))
        //     axis.x = -1.0f;
        // if (Input.GetKey(KeyCode.A))
        //     axis.y = +1.0f;
        // if (Input.GetKey(KeyCode.D))
        //     axis.y = -1.0f;

        // axis = axis.normalized;
        return axis;
    }    
}
