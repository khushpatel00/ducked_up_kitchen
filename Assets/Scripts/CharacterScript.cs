using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float movementMultiplier = 7.0f;
    private float rotationSpeed = 10f;
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

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }
}
