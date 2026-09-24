using System;
using UnityEngine;

public class CharacterScript : MonoBehaviour, IKitchenObjectParent
{
	public static CharacterScript Instance { get; private set; }
	[SerializeField] private Transform kitchenObjectHoldPoint;
	private KitchenObject kitchenObject;
	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
	public class OnSelectedCounterChangedEventArgs : EventArgs
	{
		public BaseCounter selectedCounter;
	}
	private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
	[SerializeField] private float movementMultiplier = 7.0f;
	[SerializeField] private GameObject playerVisual;
	private float rotationSpeed = 10f;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private LayerMask layerMask;
	private Vector3 lastInteractDir;
	private BaseCounter selectedCounter;
	private float playerRadius = 0.7f;
	private float playerHeight = 2.0f;
	private GameObject equipedKitchenObject;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Theres another player!!");
		}
		Instance = this;
	}

	private void Start()
	{
		if (playerVisual == null) // can be overridden from inspector
			playerVisual = GameObject.FindWithTag("PlayerVisual");

		gameInput.OnInteractAction += GameInput_OnInteractAction;
	}

	private void GameInput_OnInteractAction(object sender, EventArgs e)
	{
		if (Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerHeight), playerRadius, lastInteractDir, out RaycastHit raycastHit, 1f))
		{
			if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
			{
				counter.Interact(this);
				if (counter != selectedCounter)
					SetSelectedCounter(counter);
			}
			else
			{
				Debug.Log("EXCEPTION: Unknown Collider" + raycastHit.transform);
				SetSelectedCounter(null);
			}
		}
		else
		{
			SetSelectedCounter(null);
		}
	}


	private void Update()
	{
		HandleMovement();
		if (Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerHeight), playerRadius, lastInteractDir, out RaycastHit raycastHit, 1f))
		{
			if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
			{
				if (baseCounter != selectedCounter || selectedCounter == null)
					SetSelectedCounter(baseCounter);
			}
			else SetSelectedCounter(null);
		}
		else SetSelectedCounter(null);
	}
	private void HandleMovement()
	{

		Vector2 axis = gameInput.GetMovementVectorNormalized();
		Vector3 moveDir = new Vector3(axis.x, 0, axis.y);

		if (moveDir != Vector3.zero)
		{
			lastInteractDir = moveDir;
		}

		float movementDistance = Time.deltaTime * movementMultiplier;
		float playerRadius = 0.7f;
		float playerHeight = 2.0f;
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, movementDistance);

		// Collision Detection
		if (!canMove)
		{
			// attempt movement on x axis only
			Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, movementDistance);
			if (canMove)
				moveDir = moveDirX;
			else
			{
				// cant move on X
				// attempt on Z
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, movementDistance);
				if (canMove)
					moveDir = moveDirZ;
			}
		}


		if (canMove)
			transform.position = transform.position + moveDir * movementDistance;

		playerVisual.GetComponent<Animator>().SetBool(IsMovingHash, moveDir != Vector3.zero);

		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
	}

	private void SetSelectedCounter(BaseCounter selectedCounter)
	{
		this.selectedCounter = selectedCounter;

		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
		{
			selectedCounter = selectedCounter
		});
	}	
	public Transform GetKitchenObjectFollowTransform()
	{
		return kitchenObjectHoldPoint;
	}
	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
		equipedKitchenObject = kitchenObject.gameObject;
	}
	public KitchenObject GetKitchenObject()
	{
		return kitchenObject;
	}
	public void ClearKitchenObject(GameObject gameObject)
	{
		kitchenObject.DestroyKitchenObject(gameObject);
		kitchenObject = null;
	}
	public void ClearSelfKitchenObject()
	{
		if (equipedKitchenObject != null)
			kitchenObject.DestroyKitchenObject(equipedKitchenObject);
		kitchenObject = null;
	}
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}
