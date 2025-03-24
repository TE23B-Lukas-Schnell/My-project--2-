using UnityEngine;
using UnityEngine.InputSystem;

public class moveController : MonoBehaviour
{
    [SerializeField]
    float moveForce;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float RaycastFloorCheckLength;


    [SerializeField]
    Rigidbody rigidBody;
    Vector2 moveInput;
    bool jumpInput = false;

    float velocityY = 0;

    void Start()
    {

    }

    void FixedUpdate()
    {
        velocityY += Physics.gravity.y * Time.deltaTime;

        velocityY = 0;
        if (jumpInput && isGrounded())
        {
            velocityY = jumpForce;
            jumpInput = false;
        }

        // find target velocity
        Vector3 currentVelocity = rigidBody.velocity;
        Vector3 targetVelocity;
        targetVelocity = new Vector3(moveInput.x * moveForce, velocityY, moveInput.y * moveForce);

        // align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        // calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, velocityY, velocityChange.z);

        // add force
        rigidBody.AddForce(velocityChange, ForceMode.Impulse);
    }

    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, RaycastFloorCheckLength))return true;
        else return false;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        jumpInput = true;
    }
}
