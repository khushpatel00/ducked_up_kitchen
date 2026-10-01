using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
	public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;

	PlayerInputActions playerInputActions;
	public void Awake()
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interact_Performed;
		playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
	}

	private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
	   OnInteractAction?.Invoke(this, EventArgs.Empty);
	}
	private void InteractAlternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
	   OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized()
	{
		Vector2 axis = playerInputActions.Player.Move.ReadValue<Vector2>();
		return axis;
	}    
}
