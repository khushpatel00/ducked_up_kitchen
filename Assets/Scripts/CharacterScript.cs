using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float movementMultiplier = 7.0f;
    [SerializeField] private GameObject playerVisual;
    private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
 
    private void Start()
    {
        if (playerVisual == null) // can be overridden from inspector
            playerVisual = GameObject.FindWithTag("PlayerVisual");
    }


    private void Update() {
        
        Vector2 axis = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(axis.x, 0, axis.y);

        float movementDistance = Time.deltaTime * movementMultiplier;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight , playerRadius, moveDir, movementDistance);
        
        // Collision Detection
        if (!canMove)
        {
            // attempt movement on x axis only
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight , playerRadius, moveDirX, movementDistance);
            if (canMove)
                moveDir = moveDirX;
            else
            {
                // cant move on X
                // attempt on Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight , playerRadius, moveDirZ, movementDistance);
                if (canMove)
                    moveDir = moveDirZ;
                else
                {
                    // cant move anywhere
                }
            }
        } 


        if (canMove)
            transform.position = transform.position + moveDir * movementDistance;

        if (moveDir != Vector3.zero)
            playerVisual.GetComponent<Animator>().SetBool("isMoving", true);
        else 
            playerVisual.GetComponent<Animator>().SetBool("isMoving", false);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }
}
