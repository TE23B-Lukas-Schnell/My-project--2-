using UnityEngine;
using UnityEngine.InputSystem;

public class lookController : MonoBehaviour
{
    [SerializeField]
    Camera head;
    Vector2 lookInput;
    [SerializeField]
    Vector2 sensitivity = Vector2.one;

    float xRotation = 0;

    void Start()
    {
        head = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xRotation += lookInput.y * sensitivity.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        head.transform.localEulerAngles = new(xRotation, 0, 0);

        transform.Rotate(Vector3.up, lookInput.x * sensitivity.x);
        head.transform.Rotate(Vector3.right, lookInput.y * sensitivity.y);
    }
    void OnLook(InputValue Value)
    {
        lookInput = Value.Get<Vector2>();
    }

}
