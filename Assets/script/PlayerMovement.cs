using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;

    Vector3 moveDirection;
    Vector2 Input;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void OnMove(InputValue value)
    {
        Input = value.Get<Vector2>();

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * Input.y + orientation.right * Input.x;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
