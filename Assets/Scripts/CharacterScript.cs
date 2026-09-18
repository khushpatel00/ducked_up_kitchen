using System;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private float movementMultiplier = 7.0f;
    [SerializeField] private GameObject playerVisual;
    private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    private Vector3 lastInteractDir;

    private void Start()
    {
        if (playerVisual == null) // can be overridden from inspector
            playerVisual = GameObject.FindWithTag("PlayerVisual");

        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        float interactDistance = 0.1f;
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up, interactDistance, lastInteractDir, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            } else
            {
                Debug.Log("EXCEPTION: Unknown Collider" + raycastHit.transform);
            }
        }
    }


    private void Update()
    {
        HandleMovement();
        // Debug.DrawRay(transform.position, lastInteractDir * 2f, Color.red);
        // ManualInteract();
    }

    private void ManualInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactDistance = 2.0f;
            if (Physics.Raycast(transform.position, lastInteractDir, interactDistance))
            {
                Debug.Log("Manual Interact");
            }
            else
            {
                Debug.Log("Manual: No Collider");
            }
        }
        if (Physics.Raycast(transform.position, lastInteractDir, 0.5f))
        {
            Debug.Log("AUTO INTERACT");
        }
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
}
