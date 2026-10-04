using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private PlayerController playerInput;

    private InputAction moveAction;

    Vector2 movementDirection = Vector2.zero;
    Vector2 dragStart; 

    private void Awake()
    {
        playerInput = new PlayerController();
    }

    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Old movement
        //movementDirection = moveAction.ReadValue<Vector2>();

        Pointer pointer = Pointer.current;

        if (pointer == null) return;

        if (pointer.press.wasPressedThisFrame)
            dragStart = pointer.position.ReadValue();

        if (pointer.press.isPressed)
        {
            Vector2 drag = pointer.position.ReadValue() - dragStart;
            // Small dead zone (in pixels) so tiny jitters don't move the player
            movementDirection = drag.magnitude > 10f ? drag.normalized : Vector2.zero;
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movementDirection);
    }
}
