using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;

    [Header("Crunch")]
    public float crouchHeight = 1f;
    public float normalHeight = 2f;
    public Transform cameraHolder;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public /*static*/ bool isHidden = false;

    private Rigidbody rb;
    [SerializeField] private float currentSpeed;
    private bool isGrounded;
    private bool isCrouching;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        CheckGround();
        HandleCrouch();
        if (isCrouching == false)
        {
            HandleMovement();
        }
        HandleJump();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isCrouching == false;
        currentSpeed = isRunning ? runSpeed : walkSpeed;



        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 velocity = move * currentSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            currentSpeed = crouchSpeed;
            transform.localScale = new Vector3(1, crouchHeight / normalHeight, 1);
            cameraHolder.localPosition = new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            transform.localScale = Vector3.one;
            cameraHolder.localPosition = new Vector3(0, 0.9f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isHidden = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isHidden = false;
    }
    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void OhMyGodTheyKilledKenny_Motherfuchers()
    {

    }
}