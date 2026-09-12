using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float movementMultiplier = 7.0f;
    [SerializeField] private GameObject playerVisual;
    private float rotationSpeed = 10f;


    private void Start()
    {
        if (playerVisual == null) // can be overridden from inspector
            playerVisual = GameObject.FindWithTag("PlayerVisual");
    }


    private void Update() {
        Vector2 axis = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
            axis.x = +1.0f;
        if (Input.GetKey(KeyCode.S))
            axis.x = -1.0f;
        if (Input.GetKey(KeyCode.A))
            axis.y = +1.0f;
        if (Input.GetKey(KeyCode.D))
            axis.y = -1.0f;


        Vector3 moveDir = new Vector3(axis.x, 0, axis.y).normalized;
        transform.position = transform.position + moveDir * Time.deltaTime * movementMultiplier;

        if (moveDir != Vector3.zero)
            // GetComponent<Animator>().SetBool("isMoving", true);
            playerVisual.GetComponent<Animator>().SetBool("isMoving", true);
        else 
            // GetComponent<Animator>().SetBool("isMoving", true);
            playerVisual.GetComponent<Animator>().SetBool("isMoving", false);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }
}
