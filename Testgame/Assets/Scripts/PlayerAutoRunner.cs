using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAutoRunner : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 8f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckDistance = 1.1f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
        {
            Vector3 velocity = rb.velocity;
            velocity.y = 0f;
            rb.velocity = velocity;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = forwardSpeed;
        rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance);
    }
}
